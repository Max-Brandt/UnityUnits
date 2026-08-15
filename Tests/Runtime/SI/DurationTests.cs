using NUnit.Framework;

public class DurationTests
{
    [TestCase(1f, Duration.Units.Day, Duration.Units.Hour, 24f)]
    [TestCase(1f, Duration.Units.Hour, Duration.Units.Minute, 60f)]
    [TestCase(90f, Duration.Units.Minute, Duration.Units.Hour, 1.5f)]
    [TestCase(1f, Duration.Units.Secound, Duration.Units.Millisecound, 1000f)]
    [TestCase(1f, Duration.Units.Millisecound, Duration.Units.Microsecound, 1000f)]
    [TestCase(1f, Duration.Units.Day, Duration.Units.Secound, 86_400f)]
    [TestCase(1f, Duration.Units.Secound, Duration.Units.Secound, 1f)]
    [TestCase(0f, Duration.Units.Day, Duration.Units.Microsecound, 0f)]
    public void To_ConvertsBetweenUnits(float value, Duration.Units from, Duration.Units to, float expected)
    {
        var duration = new Duration(value, from);

        Assert.That(duration.To(to), Is.EqualTo(expected).Within(1e-2f));
    }

    [Test]
    public void DefaultConstructor_UsesSecoundAsUnit()
    {
        var duration = new Duration(3f);

        Assert.That(duration.unit, Is.EqualTo(Duration.Units.Secound));
    }

    [Test]
    public void ConvertDuration_ReturnsStructWithConvertedValueAndTargetUnit()
    {
        var result = Duration.ConvertDuration(2f, Duration.Units.Hour, Duration.Units.Minute);

        Assert.That(result.value, Is.EqualTo(120f).Within(1e-3f));
        Assert.That(result.unit, Is.EqualTo(Duration.Units.Minute));
    }
}
