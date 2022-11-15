namespace VetAppointment.Model;
using VetAppointment.Util;

public class Cage {
    public Guid ID { get; private set; }
    public Size Size { get; private set; }

    public Guid clinicId { get; private set; }

    private Cage(Size size) {
        this.ID = new Guid();
        this.Size = size;
    }

    public static Result<Cage> Create(string size)
    {
        Result<Size> sizeResult = SizeExtensions.FromString(size);
        
        if( sizeResult.IsFailure ) {
            return Result<Cage>.Failure($"Failed to create Cage with size '{size}'. {sizeResult.Error}");
        }

        return Result<Cage>.Success(new Cage(sizeResult.Entity));
    }

    public void AssignToClinic(Guid locationId)
    {
        this.clinicId = locationId;
    }
}