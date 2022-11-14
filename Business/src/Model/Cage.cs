namespace VetAppointment.Model;
using VetAppointment.Util;

class Cage {
    public Guid ID { get; }
    public Size Size { get; }

    private Cage(Guid id, Size size) {
        this.ID = id;
        this.Size = size;
    }

    public static Result<Cage> Create(string size)
    {
        Size s;
        if(!Size.TryParse(size, out s)) {
            return Result<Cage>.Failure(s + " is not a valid size.");
        }
        
        return Result<Cage>.Success(new Cage(Guid.NewGuid(), s));
    }
}