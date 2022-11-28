using System;
using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;
public class MedicalRecordTests
{
    [Fact]
    public void When_CreateMedicalRecord_Then_ShouldReturnMedicalRecord()
    {
        //Arrange
        var sut = CreateSUT();
        
        //Act
        var result = MedicalRecord.Create(sut.Item1, sut.Item2);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.IdAppointment.Should().Be(sut.Item1);
        result.Value.IdClient.Should().Be(sut.Item2);
        result.Value.Id.Should().NotBeEmpty();
    }
    [Fact]
    public void When_CreateMedicalRecordWithEmpyIdAppointment_Then_ShouldReturnFail()
    {
        //Arrange
        var idAppointment = Guid.Empty;
        var idClient = Guid.NewGuid();
        //Act
        var result = MedicalRecord.Create(idAppointment, idClient);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Id appointment can not be empty");
    }
    [Fact]
    public void When_CreateMedicalRecordWithEmpyIdClient_Then_ShouldReturnFail()
    {
        //Arrange
        var idAppointment = Guid.NewGuid();
        var idClient = Guid.Empty;
        //Act
        var result = MedicalRecord.Create(idAppointment, idClient);

        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Id client can not be empty");
    }
    private Tuple<Guid, Guid> CreateSUT()
    {
        Tuple<Guid, Guid> sut = new(Guid.NewGuid(), Guid.NewGuid());
        return sut;
    }
}
