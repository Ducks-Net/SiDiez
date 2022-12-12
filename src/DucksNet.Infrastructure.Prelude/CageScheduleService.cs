using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;

public class  CageScheduleService 
{
    private readonly IRepositoryAsync<Cage> _cagesRepository;
    private readonly IRepositoryAsync<CageTimeBlock> _cageTimeBlocksRepository;
    private readonly IRepositoryAsync<Pet> _petsRepository;

    public CageScheduleService(IRepositoryAsync<Cage> cages, IRepositoryAsync<CageTimeBlock> cageTimeBlocks, IRepositoryAsync<Pet> pets)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
    }

    public List<CageTimeBlock> GetLocationSchedule(Guid locationId)
    {
        var cages = _cagesRepository.GetAllAsync().Result.Where(c => c.LocationId == locationId);
        var res = _cageTimeBlocksRepository.GetAllAsync().Result.Join(cages, ct => ct.CageId, c => c.ID, (ct, c) => ct).ToList();

        return res;
    }

    public List<CageTimeBlock> GetPetSchedule(Guid petId)
    {
        var pet = _petsRepository.GetAsync(petId).Result;
        var res = _cageTimeBlocksRepository.GetAllAsync().Result.Where(ctb => ctb.OccupantId == petId).ToList();

        return res;
    }

    public Result<CageTimeBlock> ScheduleCage(Guid petId, Guid locationId, DateTime startTime, DateTime endTime)
    {
        Result<Pet> pet = _petsRepository.GetAsync(petId).Result;

        // check if pet exists
        if (pet.IsFailure)
        {
            return Result<CageTimeBlock>.FromError(pet, "Pet not found in repository.");
        }

        List<Cage> locationCages = _cagesRepository.GetAllAsync().Result.Where(c => c.LocationId == locationId).ToList();
        List<CageTimeBlock> locationTimeBlocks = _cageTimeBlocksRepository
                                                 .GetAllAsync().Result
                                                 .Where(t => t.CageId != null)
                                                 .Where(c => locationCages.Select(c => c.ID).ToList().Contains(c.CageId!.Value))
                                                 .ToList();

        // check if the pet is already scheduled at that time
        // TODO (AL) : Also check the pet isn't at an appointment at that time
        List<CageTimeBlock> petTimeBlocksColisions = _cageTimeBlocksRepository
                                                     .GetAllAsync().Result
                                                     .Where(t => t.OccupantId != null)
                                                     .Where(t => t.OccupantId == petId)
                                                     .Where(t => t.StartTime <= startTime && t.EndTime >= endTime)
                                                     .ToList(); 
        if (petTimeBlocksColisions.Count > 0)
        {
            return Result<CageTimeBlock>.Error("Pet is already scheduled for that time.");
        }

        // Check if there are appropriate cages available at that location
        List<Cage> cages = locationCages
                           .Where(c => locationTimeBlocks
                                       .Where(t => t.CageId != null)
                                       .Where(t => t.CageId == c.ID)
                                       .Where(t => t.StartTime <= startTime && t.EndTime >= endTime)
                                       .Any())
                           .Where(c => c.Size == pet.Value!.Size)
                           .ToList();

        if (cages.Count == 0)
        {
            return Result<CageTimeBlock>.Error("No cages available at that time.");
        }

        // Create a new time block for the pet
        Result<CageTimeBlock> timeBlock = CageTimeBlock.Create(startTime, endTime);
        if (timeBlock.IsFailure || timeBlock.Value == null)
        {
            return Result<CageTimeBlock>.FromError(timeBlock, "Failed to create time block.");
        }
        timeBlock.Value.AssignToCage(cages[0].ID);
        timeBlock.Value.AssignToOccupant(petId);

        _cageTimeBlocksRepository.AddAsync(timeBlock.Value);

        return Result<CageTimeBlock>.Ok(timeBlock.Value);
    }
}
