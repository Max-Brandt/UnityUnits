using NUnit.Framework;

public class TemperatureTests
{
    // Fixpunkte: Gefrierpunkt und Siedepunkt von Wasser, sowie 0 K.
    [TestCase(0f, Temperature.Units.Celsius, Temperature.Units.Kelvin, 273.15f)]
    [TestCase(100f, Temperature.Units.Celsius, Temperature.Units.Kelvin, 373.15f)]
    [TestCase(273.15f, Temperature.Units.Kelvin, Temperature.Units.Celsius, 0f)]
    [TestCase(0f, Temperature.Units.Kelvin, Temperature.Units.Celsius, -273.15f)]
    [TestCase(32f, Temperature.Units.Fahrenheit, Temperature.Units.Celsius, 0f)]
    [TestCase(212f, Temperature.Units.Fahrenheit, Temperature.Units.Celsius, 100f)]
    [TestCase(32f, Temperature.Units.Fahrenheit, Temperature.Units.Kelvin, 273.15f)]
    [TestCase(98.6f, Temperature.Units.Fahrenheit, Temperature.Units.Celsius, 37f)]
    [TestCase(-40f, Temperature.Units.Fahrenheit, Temperature.Units.Celsius, -40f)] // Schnittpunkt beider Skalen
    [TestCase(25f, Temperature.Units.Celsius, Temperature.Units.Celsius, 25f)]
    public void To_ConvertsBetweenUnits(float value, Temperature.Units from, Temperature.Units to, float expected)
    {
        var temperature = new Temperature(value, from);

        Assert.That(temperature.To(to), Is.EqualTo(expected).Within(1e-2f));
    }

    [Test]
    public void DefaultConstructor_UsesKelvinAsUnit()
    {
        var temperature = new Temperature(3f);

        Assert.That(temperature.unit, Is.EqualTo(Temperature.Units.Kelvin));
    }

    [Test]
    public void RoundTripConversion_CelsiusToFahrenheitToCelsius_ReturnsOriginalValue()
    {
        var temperature = new Temperature(21.5f, Temperature.Units.Celsius);

        var asFahrenheit = temperature.To(Temperature.Units.Fahrenheit);
        var backToCelsius = Temperature.Convert(asFahrenheit, Temperature.Units.Fahrenheit, Temperature.Units.Celsius);

        Assert.That(backToCelsius, Is.EqualTo(temperature.value).Within(1e-2f));
    }

    [Test]
    public void ConvertTemperature_ReturnsStructWithConvertedValueAndTargetUnit()
    {
        var result = Temperature.ConvertTemperature(0f, Temperature.Units.Celsius, Temperature.Units.Kelvin);

        Assert.That(result.value, Is.EqualTo(273.15f).Within(1e-2f));
        Assert.That(result.unit, Is.EqualTo(Temperature.Units.Kelvin));
    }
}
