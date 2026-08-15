using NUnit.Framework;

public class MassTests
{
    [TestCase(1f, Mass.Units.Ton, Mass.Units.Kilogramm, 1000f)]
    [TestCase(1000f, Mass.Units.Gramm, Mass.Units.Kilogramm, 1f)]
    [TestCase(1f, Mass.Units.Kilogramm, Mass.Units.Milligramm, 1_000_000f)]
    [TestCase(1f, Mass.Units.Kilogramm, Mass.Units.Gramm, 1000f)]
    [TestCase(2.5f, Mass.Units.Ton, Mass.Units.Gramm, 2_500_000f)]
    [TestCase(1f, Mass.Units.Kilogramm, Mass.Units.Kilogramm, 1f)]
    [TestCase(0f, Mass.Units.Ton, Mass.Units.Milligramm, 0f)]
    public void To_ConvertsBetweenUnits(float value, Mass.Units from, Mass.Units to, float expected)
    {
        var mass = new Mass(value, from);

        Assert.That(mass.To(to), Is.EqualTo(expected).Within(1e-2f));
    }

    [Test]
    public void DefaultConstructor_UsesKilogrammAsUnit()
    {
        var mass = new Mass(3f);

        Assert.That(mass.unit, Is.EqualTo(Mass.Units.Kilogramm));
    }

    [Test]
    public void ConvertMass_ReturnsStructWithConvertedValueAndTargetUnit()
    {
        var result = Mass.ConvertMass(1f, Mass.Units.Ton, Mass.Units.Kilogramm);

        Assert.That(result.value, Is.EqualTo(1000f).Within(1e-3f));
        Assert.That(result.unit, Is.EqualTo(Mass.Units.Kilogramm));
    }

    [Test]
    public void MultiplicationWithAcceleration_ReturnsForceInNewton()
    {
        var mass = new Mass(2f, Mass.Units.Kilogramm);
        var acceleration = new Acceleration(3f, Acceleration.Units.MeterPerSquareSecound);

        var force = mass * acceleration;

        Assert.That(force.unit, Is.EqualTo(Force.Units.Newton));
        Assert.That(force.value, Is.EqualTo(6f).Within(1e-4f));
    }

    [Test]
    public void DivisionByVolume_ReturnsDensityInKilogramPerCubicMeter()
    {
        var mass = new Mass(10f, Mass.Units.Kilogramm);
        var volume = new Volume(2f, Volume.Units.CubicMeter);

        var density = mass / volume;

        Assert.That(density.unit, Is.EqualTo(Density.Units.KilogramPerCubicMeter));
        Assert.That(density.value, Is.EqualTo(5f).Within(1e-4f));
    }
}
