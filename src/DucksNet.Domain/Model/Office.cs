using DucksNet.Domain.Model.Enums;
using DucksNet.SharedKernel.Utils;

namespace DucksNet.Domain.Model;
public class Office
{
    public Guid ID { get; private set; }
    public Guid BusinessId { get; set; }
    public string Address {get; private set;}
    public int AnimalCapacity {get; set;}
    
    private Office(Guid businessId, string address, int animalCapacity) 
    {
        ID = new Guid();
        BusinessId = businessId;
        Address = address;
        AnimalCapacity = animalCapacity;
    }
    public static Result<Office> Create(Guid businessId, string address, int animalCapacity)
    {
        if(businessId == null || businessId == Guid.Empty)
            return Result<Office>.Error("Business ID is required");
        if(address == null || address == string.Empty)
            return Result<Office>.Error("Address is required");
        if(animalCapacity == -1)
            return Result<Office>.Error("Animal capacity is required");
        return Result<Office>.Ok(new Office(businessId, address, animalCapacity));
    }
}
