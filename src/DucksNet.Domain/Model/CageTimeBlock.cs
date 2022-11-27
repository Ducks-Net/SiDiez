using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;

public class CageTimeBlock
{
    public Guid Id { get; private set; }
    public Guid? CageId { get; private set; }
    public Guid? OccupantId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    
    public CageTimeBlock(Guid id, Guid? cageId, Guid? occupantId, DateTime startTime, DateTime endTime) // TODO (Al): just for json parsing. to remove
    {
        Id = id;
        CageId = cageId;
        OccupantId = occupantId;
        StartTime = startTime;
        EndTime = endTime;
    }

    private CageTimeBlock(DateTime startTime, DateTime endTime)
    {
        Id = new Guid();
        StartTime = startTime;
        EndTime = endTime;
    }

    public static Result<CageTimeBlock> Create(DateTime start, DateTime end)
    {
        if (start >= end)
            return Result<CageTimeBlock>.Error("Time block start time must be before end time.");

        return Result<CageTimeBlock>.Ok(new CageTimeBlock(start, end));
    }

    public void AssignToCage(Guid cageId)
    {
        CageId = cageId;
    }
    public void AssignToOccupant(Guid occupantId)
    {
        OccupantId = occupantId;
    }
}
