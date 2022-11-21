using System;
using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;
public class MedicalRecordTests
{
    [Fact]
    public void When_CreateMedicalRecord_Then_ShouldReturnMedicalRecord()
    {
        //Arrange
        var idAppointment = Guid.NewGuid();
        var idClient = Guid.NewGuid();
        
        //Act
        var result = MedicalRecord.Create(idAppointment, idClient);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        var copy = result.Value;
        if (copy is not null)
        {
            copy.IdAppointment.Should().Be(idAppointment);
            copy.IdClient.Should().Be(idClient);
            copy.Id.Should().NotBeEmpty();
        }
    }
}
