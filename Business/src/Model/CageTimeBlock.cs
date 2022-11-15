namespace VetAppointment.Model;
using VetAppointment.Util;


public class CageTimeBlock
{
    public Guid ID { get; private set; }
    public Guid CageID { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    private CageTimeBlock(DateTime start, DateTime end)
    {
        this.ID = new Guid();
        this.Start = start;
        this.End = end;
    }

    public static Result<CageTimeBlock> Create(DateTime start, DateTime end) 
    {
        if( end < start) {
            return Result<CageTimeBlock>.Failure($"Failed to create CageTimeBlock. End time ({end}) cannot be before the start time ({start}).");
        }

        return Result<CageTimeBlock>.Success(new CageTimeBlock(start, end));
    }

    public void AssignToCage(Guid cageID)
    {
        this.CageID = cageID;
    }
}