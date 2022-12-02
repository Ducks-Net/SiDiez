using DucksNet.Domain.Model;

public class TreatmentTests
{
    [Fact]
    public void When_CreateTreatments_WithAtLeastOneMedicine_Should_Succeed()
    {
        string name = "Name";
        string description = "Description";
        double price = 1;
        string administration = "Oral";
        var medicine = Medicine.Create(name, description, price, administration);
        var result = Treatment.CreateTreatment();
        result.Value!.AddMedicineToTreatment(medicine.Value!);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
