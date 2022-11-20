using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;


public class CageTimeBlock
{
    public Guid Id { get; }
    public Guid? CageId { get; private set; }
    public Guid? OccupantId { get; private set; }
    public DateTime Start { get; }
    public DateTime End { get; }
    
    private CageTimeBlock(DateTime start, DateTime end)
    {
        Id = new Guid();
        Start = start;
        End = end;
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
