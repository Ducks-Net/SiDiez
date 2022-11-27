using DucksNet.Domain.Model;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Infrastructure.Prelude;

public class  CageScheduleService 
{
    private readonly IRepository<Cage> _cagesRepository;
    private readonly IRepository<CageTimeBlock> _cageTimeBlocksRepository;
    private readonly IRepository<Pet> _petsRepository;

    public CageScheduleService(IRepository<Cage> cages, IRepository<CageTimeBlock> cageTimeBlocks, IRepository<Pet> pets)
    {
        _cagesRepository = cages;
        _cageTimeBlocksRepository = cageTimeBlocks;
        _petsRepository = pets;
    }

    public Result<List<CageTimeBlock>> GetLocationSchedule(Guid locationId)
    {
        var cages = _cagesRepository.GetAll().Where(c => c.LocationId == locationId);
        if (cages is null)
        {
            return Result<List<CageTimeBlock>>.Error("Location not found.");
        }

        var res = _cageTimeBlocksRepository.GetAll().Where(ctb => cages.Select(cage => cage.ID).Contains(ctb.Id)).ToList();
        return Result<List<CageTimeBlock>>.Ok(res);
    }

    public Result<List<CageTimeBlock>> GetPetSchedule(Guid petId)
    {
        var pet = _petsRepository.Get(petId);
        if (pet.IsFailure)
        {
            return Result<List<CageTimeBlock>>.Error("Pet not found in repository.");
        }
        var res = _cageTimeBlocksRepository.GetAll().Where(ctb => ctb.OccupantId == petId).ToList();
        return Result<List<CageTimeBlock>>.Ok(res);
    }

    public Result<CageTimeBlock> ScheduleCage(Guid petId, Guid locationId, DateTime startTime, DateTime endTime)
    {
        Result<Pet> pet = _petsRepository.Get(petId);

        // check if pet exists
        if (pet.IsFailure)
        {
            return Result<CageTimeBlock>.FromError(pet, "Pet not found in repository.");
        }

        List<Cage> locationCages = _cagesRepository.GetAll().Where(c => c.LocationId == locationId).ToList();
        List<CageTimeBlock> locationTimeBlocks = _cageTimeBlocksRepository
                                                 .GetAll()
                                                 .Where(t => t.CageId != null)
                                                 .Where(c => locationCages.Select(c => c.ID).ToList().Contains(c.CageId!.Value))
                                                 .ToList();

        // check if the pet is already scheduled at that time
        // TODO (AL) : Also check the pet isn't at an appointment at that time
        List<CageTimeBlock> petTimeBlocksColisions = _cageTimeBlocksRepository
                                                     .GetAll()
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
                                       .Count() == 0)
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

        _cageTimeBlocksRepository.Add(timeBlock.Value);

        return Result<CageTimeBlock>.Ok(timeBlock.Value);
    }
}
