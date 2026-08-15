using NUnit.Framework;

public class LengthTests
{
    [TestCase(1f, Length.Units.Kilometer, Length.Units.Meter, 1000f)]
    [TestCase(500f, Length.Units.Centimeter, Length.Units.Meter, 5f)]
    [TestCase(1f, Length.Units.Meter, Length.Units.Millimeter, 1000f)]
    [TestCase(2f, Length.Units.Meter, Length.Units.Kilometer, 0.002f)]
    [TestCase(1f, Length.Units.Meter, Length.Units.Micrometer, 1_000_000f)]
    [TestCase(1f, Length.Units.Meter, Length.Units.Nanometer, 1_000_000_000f)]
    [TestCase(1f, Length.Units.Meter, Length.Units.Meter, 1f)]
    [TestCase(-5f, Length.Units.Meter, Length.Units.Centimeter, -500f)]
    [TestCase(0f, Length.Units.Kilometer, Length.Units.Millimeter, 0f)]
    public void To_ConvertsBetweenUnits(float value, Length.Units from, Length.Units to, float expected)
    {
        var length = new Length(value, from);

        Assert.That(length.To(to), Is.EqualTo(expected).Within(1e-3f));
    }

    [Test]
    public void StaticConvert_MatchesInstanceTo()
    {
        var viaStatic = Length.Convert(1f, Length.Units.Kilometer, Length.Units.Centimeter);

        Assert.That(viaStatic, Is.EqualTo(100_000f).Within(1e-3f));
    }

    [Test]
    public void ConvertLength_ReturnsStructWithConvertedValueAndTargetUnit()
    {
        var result = Length.ConvertLength(1f, Length.Units.Kilometer, Length.Units.Meter);

        Assert.That(result.value, Is.EqualTo(1000f).Within(1e-3f));
        Assert.That(result.unit, Is.EqualTo(Length.Units.Meter));
    }

    [Test]
    public void ToLength_ReturnsStructWithConvertedValueAndTargetUnit()
    {
        var length = new Length(1f, Length.Units.Meter);

        var result = length.ToLength(Length.Units.Centimeter);

        Assert.That(result.value, Is.EqualTo(100f).Within(1e-3f));
        Assert.That(result.unit, Is.EqualTo(Length.Units.Centimeter));
    }

    [Test]
    public void DefaultConstructor_UsesMeterAsUnit()
    {
        var length = new Length(3f);

        Assert.That(length.unit, Is.EqualTo(Length.Units.Meter));
        Assert.That(length.value, Is.EqualTo(3f));
    }

    [Test]
    public void RoundTripConversion_ReturnsOriginalValue()
    {
        var length = new Length(123.456f, Length.Units.Millimeter);

        var roundTripped = length.To(Length.Units.Kilometer);
        var backAgain = Length.Convert(roundTripped, Length.Units.Kilometer, Length.Units.Millimeter);

        Assert.That(backAgain, Is.EqualTo(length.value).Within(1e-2f));
    }

    [Test]
    public void DivisionByDuration_ReturnsVelocityInMeterPerSecound()
    {
        var length = new Length(100f, Length.Units.Meter);
        var duration = new Duration(4f, Duration.Units.Secound);

        var velocity = length / duration;

        Assert.That(velocity.unit, Is.EqualTo(Velocity.Units.MeterPerSecound));
        Assert.That(velocity.value, Is.EqualTo(25f).Within(1e-4f));
    }
}
