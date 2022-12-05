using System;
using DucksNet.Domain.Model;

namespace DucksNet.UnitTests;
public class TreatmentTests
{

    [Fact]
    public void When_CreateTreatmentWithEmptyOwnerID_Then_ShouldFail()
    {
        Tuple<Guid, Guid, Guid> sut = CreateSUT();
        var result = Treatment.CreateTreatment(Guid.Empty, sut.Item2, sut.Item3);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Owner ID can not be empty");
    }
    [Fact]
    public void When_CreateTreatmentWithEmptyClientID_Then_ShouldFail()
    {
        Tuple<Guid, Guid, Guid> sut = CreateSUT();
        var result = Treatment.CreateTreatment(sut.Item1, Guid.Empty, sut.Item3);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Client ID can not be empty");
    }
    [Fact]
    public void When_CreateTreatmentWithEmptyClinicID_Then_ShouldFail()
    {
        Tuple<Guid, Guid, Guid> sut = CreateSUT();
        var result = Treatment.CreateTreatment(sut.Item1, sut.Item2, Guid.Empty);
        //Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().Contain("Clinic ID can not be empty");
    }
    private static Tuple<Guid, Guid, Guid> CreateSUT()
    {
        Tuple<Guid, Guid, Guid> sut = new(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        return sut;
    }
}
