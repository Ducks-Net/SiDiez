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

    public async Task<List<CageTimeBlock>> GetLocationSchedule(Guid locationId)
    {
        var cages = await _cagesRepository.GetAllAsync();
        cages = cages.Where(c => c.LocationId == locationId);
        var res = await _cageTimeBlocksRepository.GetAllAsync();
        var resList = res.Join(cages, ct => ct.CageId, c => c.ID, (ct, c) => ct).ToList();

        return resList;
    }

    public async Task<List<CageTimeBlock>> GetPetSchedule(Guid petId)
    {
        var res = await _cageTimeBlocksRepository.GetAllAsync();
        var resList = res.Where(ctb => ctb.OccupantId == petId).ToList();

        return resList;
    }

    public async Task<Result<CageTimeBlock>> ScheduleCage(Guid petId, Guid locationId, DateTime startTime, DateTime endTime)
    {
        Result<Pet> pet = await _petsRepository.GetAsync(petId);

        // check if pet exists
        if (pet.IsFailure)
        {
            return Result<CageTimeBlock>.FromError(pet, "Pet not found in repository.");
        }

        var locationCagesEnum = await _cagesRepository.GetAllAsync();
        var locationCages = locationCagesEnum.Where(c => c.LocationId == locationId).ToList();
        var locationTimeBlocksEnum = await _cageTimeBlocksRepository.GetAllAsync();
        var locationTimeBlocks = locationTimeBlocksEnum
                                     .Where(t => t.CageId != null)
                                     .Where(c => locationCages.Select(c => c.ID).ToList().Contains(c.CageId!.Value))
                                     .ToList();

        // check if the pet is already scheduled at that time
        // NOTE (AL) : Also check the pet isn't at an appointment at that time
        var petTimeBlocksCollisions = await _cageTimeBlocksRepository.GetAllAsync();
        var petTimeBlocksCollisionsList = petTimeBlocksCollisions.Where(t => t.OccupantId != null)
            .Where(t => t.OccupantId == petId)
            .Where(t => t.StartTime <= startTime && t.EndTime >= endTime)
            .ToList();
        if (petTimeBlocksCollisionsList.Count > 0)
        {
            return Result<CageTimeBlock>.Error("Pet is already scheduled for that time.");
        }

        // Check if there are appropriate cages available at that location
        List<Cage> cages = locationCages
                           .Where(c => !locationTimeBlocks
                               .Where(t => t.CageId != null)
                               .Where(t => t.CageId == c.ID)
                               .Any(t => (t.StartTime <= startTime && t.EndTime >= endTime)))
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

        await _cageTimeBlocksRepository.AddAsync(timeBlock.Value);

        return Result<CageTimeBlock>.Ok(timeBlock.Value);
    }
}
