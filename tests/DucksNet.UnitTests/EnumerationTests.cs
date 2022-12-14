using DucksNet.SharedKernel.Utils;

namespace DucksNet.UnitTests;

public class EnumerationTests 
{
    // We're gonna make a testing enumeration for this test
    private class TestEnumeration : Enumeration
    {
        public static readonly TestEnumeration One = new(1, "One");
        public static readonly TestEnumeration Two = new(2, "Two");
        public static readonly TestEnumeration Three = new(3, "Three");

        private TestEnumeration(int id, string name) : base(id, name) { }
    }

    [Fact]
    public void When_GetAll_Should_ReturnAllValues()
    {
        var result = TestEnumeration.GetAll<TestEnumeration>();

        result.Should().NotBeEmpty();
        result.Should().HaveCount(3);
        result.Should().Contain(TestEnumeration.One);
        result.Should().Contain(TestEnumeration.Two);
        result.Should().Contain(TestEnumeration.Three);
    }

    [Fact]
    public void When_GetName_ShouldReturnName()
    {
        var one = TestEnumeration.One.Name;
        var two = TestEnumeration.Two.Name;
        var three = TestEnumeration.Three.Name;

        one.Should().Be("One");
        two.Should().Be("Two");
        three.Should().Be("Three");
    }

    [Fact]
    public void When_GetId_ShouldReturnId()
    {
        var one = TestEnumeration.One.Id;
        var two = TestEnumeration.Two.Id;
        var three = TestEnumeration.Three.Id;

        one.Should().Be(1);
        two.Should().Be(2);
        three.Should().Be(3);
    }

    [Fact]
    public void ToString_ShouldReturnName()
    {
        var one = TestEnumeration.One.ToString();
        var two = TestEnumeration.Two.ToString();
        var three = TestEnumeration.Three.ToString();

        one.Should().Be(TestEnumeration.One.Name);
        two.Should().Be(TestEnumeration.Two.Name);
        three.Should().Be(TestEnumeration.Three.Name);
    }

    [Fact]
    public void When_Equals_WithItself_ShouldReturnTrue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.Equals(one).Should().BeTrue();
        two.Equals(two).Should().BeTrue();
        three.Equals(three).Should().BeTrue();
    }

    [Fact]
    public void When_Equals_WithOther_ShouldReturnFalse()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.Equals(two).Should().BeFalse();
        one.Equals(three).Should().BeFalse();
        two.Equals(three).Should().BeFalse();
    }

    [Fact]
    public void When_Equals_WithNull_ShouldReturnFalse()
    {
        var one = TestEnumeration.One;

        one.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void When_Equals_WithOtherType_ShouldReturnFalse()
    {
        var one = TestEnumeration.One;

        one.Equals("One").Should().BeFalse();
    }

    [Fact]
    public void When_EqualsOperator_WithItself_ShouldReturnTrue()
    {
        var one = TestEnumeration.One;
        var one2 = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var two2 = TestEnumeration.Two;
        var three = TestEnumeration.Three;
        var three2 = TestEnumeration.Three;

        (one == one2).Should().BeTrue();
        (two == two2).Should().BeTrue();
        (three == three2).Should().BeTrue();
    }

    [Fact]
    public void When_EqualsOperator_WithOther_ShouldReturnFalse()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        (one == two).Should().BeFalse();
        (one == three).Should().BeFalse();
        (two == three).Should().BeFalse();
    }

    [Fact]
    public void When_GetHashCode_WithItself_ShouldReturnSameValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.GetHashCode().Should().Be(one.GetHashCode());
        two.GetHashCode().Should().Be(two.GetHashCode());
        three.GetHashCode().Should().Be(three.GetHashCode());
    }

    [Fact]
    public void When_GetHashCode_WithOther_ShouldReturnDifferentValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.GetHashCode().Should().NotBe(two.GetHashCode());
        one.GetHashCode().Should().NotBe(three.GetHashCode());
        two.GetHashCode().Should().NotBe(three.GetHashCode());
    }

    [Fact]
    public void When_Compared_WithItself_ShouldReturnZero()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.CompareTo(one).Should().Be(0);
        two.CompareTo(two).Should().Be(0);
        three.CompareTo(three).Should().Be(0);
    }

    [Fact]
    public void When_Compared_WithOther_ShouldReturnCorrectValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        one.CompareTo(two).Should().Be(-1);
        one.CompareTo(three).Should().Be(-1);
        two.CompareTo(three).Should().Be(-1);

        two.CompareTo(one).Should().Be(1);
        three.CompareTo(one).Should().Be(1);
        three.CompareTo(two).Should().Be(1);
    }

    [Fact]
    public void When_Compared_WithNull_ShouldReturnOne()
    {
        var one = TestEnumeration.One;

        one.CompareTo(null).Should().Be(1);
    }

    [Fact]
    public void When_NotEqualsOperator_With_ITself_ShouldReturnFalse()
    {
        var one = TestEnumeration.One;
        var one2 = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var two2 = TestEnumeration.Two;
        var three = TestEnumeration.Three;
        var three2 = TestEnumeration.Three;

        (one != one2).Should().BeFalse();
        (two != two2).Should().BeFalse();
        (three != three2).Should().BeFalse();
    }

    [Fact]
    public void When_NotEqualsOperator_WithOther_ShouldReturnTrue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        (one != two).Should().BeTrue();
        (one != three).Should().BeTrue();
        (two != three).Should().BeTrue();
    }

    [Fact]
    public void When_GreaterThanOperator_WithOther_ShouldReturnCorrectValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        (one > two).Should().BeFalse();
        (one > three).Should().BeFalse();
        (two > three).Should().BeFalse();

        (two > one).Should().BeTrue();
        (three > one).Should().BeTrue();
        (three > two).Should().BeTrue();
    }

    [Fact]
    public void When_LessThanOperator_WithOther_ShouldReturnCorrectValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;

        (one < two).Should().BeTrue();
        (one < three).Should().BeTrue();
        (two < three).Should().BeTrue();

        (two < one).Should().BeFalse();
        (three < one).Should().BeFalse();
        (three < two).Should().BeFalse();
    }

    [Fact]
    public void When_GreaterThanOrEqualOperator_WithOther_ShouldReturnCorrectValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;
        var one2 = TestEnumeration.One;
        var two2 = TestEnumeration.Two;
        var three2 = TestEnumeration.Three;

        (one >= two).Should().BeFalse();
        (one >= three).Should().BeFalse();
        (two >= three).Should().BeFalse();

        (two >= one).Should().BeTrue();
        (three >= one).Should().BeTrue();
        (three >= two).Should().BeTrue();

        (one >= one2).Should().BeTrue();
        (two >= two2).Should().BeTrue();
        (three >= three2).Should().BeTrue();
    }

    [Fact]
    public void When_LessThanOrEqualOperator_WithOther_ShouldReturnCorrectValue()
    {
        var one = TestEnumeration.One;
        var two = TestEnumeration.Two;
        var three = TestEnumeration.Three;
        var one2 = TestEnumeration.One;
        var two2 = TestEnumeration.Two;
        var three2 = TestEnumeration.Three;

        (one <= two).Should().BeTrue();
        (one <= three).Should().BeTrue();
        (two <= three).Should().BeTrue();

        (two <= one).Should().BeFalse();
        (three <= one).Should().BeFalse();
        (three <= two).Should().BeFalse();

        (one <= one2).Should().BeTrue();
        (two <= two2).Should().BeTrue();
        (three <= three2).Should().BeTrue();
    }
}
