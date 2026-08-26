using NUnit.Framework;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Prüft die Reflection-Logik in <see cref="PhysicalValueDrawer.ConvertValue"/>: findet sie für
/// jede Quantity-Struct das verschachtelte "Units"-Enum und die statische "Convert(float, Units, Units)"-
/// Methode korrekt, und rechnet sie tatsächlich richtig um? Die eigentliche UI (CreatePropertyGUI,
/// PopupField-Verdrahtung) wird hier bewusst NICHT getestet.
/// </summary>
public class PhysicalValueDrawerTests
{
    private class TestHost : ScriptableObject
    {
        public Length length;
        public Mass mass;
        public Duration duration;
        public Temperature temperature;
        public Density density;
        public Force force;
        public Torque torque;
        public Velocity velocity;
        public Angle angle;
        public Area area;
        public Volume volume;
        public Impulse impulse;
        public Acceleration acceleration;
        public Jerk jerk;
        public HeatFlux heatFlux;
        public HeatTransferCoefficient heatTransferCoefficient;
        public ThermalConductivity thermalConductivity;
        public ThermalResistance thermalResistance;
    }

    private TestHost _host;
    private SerializedObject _serializedObject;

    [SetUp]
    public void SetUp()
    {
        _host = ScriptableObject.CreateInstance<TestHost>();
        _serializedObject = new SerializedObject(_host);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_host);
    }

    private SerializedProperty Property(string fieldName) => _serializedObject.FindProperty(fieldName);

    [Test]
    public void Length_KilometerToMeter()
    {
        // Units: Kilometer=0, Meter=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.length)), 1f, 0, 1);
        Assert.That(result, Is.EqualTo(1000f).Within(1e-3f));
    }

    [Test]
    public void Mass_TonToKilogramm()
    {
        // Units: Ton=0, Kilogramm=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.mass)), 1f, 0, 1);
        Assert.That(result, Is.EqualTo(1000f).Within(1e-3f));
    }

    [Test]
    public void Duration_DayToHour()
    {
        // Units: Day=0, Hour=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.duration)), 1f, 0, 1);
        Assert.That(result, Is.EqualTo(24f).Within(1e-3f));
    }

    [Test]
    public void Temperature_CelsiusToKelvin()
    {
        // Units: Celsius=0, Kelvin=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.temperature)), 0f, 0, 1);
        Assert.That(result, Is.EqualTo(273.15f).Within(1e-2f));
    }

    [Test]
    public void Density_GrammPerCubicCentimeterToKilogramPerCubicDecimeter()
    {
        // Units: GrammPerCubicCentimeter=0, KilogramPerCubicDecimeter=2 - physikalisch aequivalent (1:1),
        // unabhaengig vom bekannten Skalierungsfehler gegenueber KilogramPerCubicMeter.
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.density)), 5f, 0, 2);
        Assert.That(result, Is.EqualTo(5f).Within(1e-3f));
    }

    [Test]
    public void Force_KiloNewtonToNewton()
    {
        // Units: Newton=0, KiloNewton=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.force)), 1f, 1, 0);
        Assert.That(result, Is.EqualTo(1000f).Within(1e-3f));
    }

    [Test]
    public void Torque_KiloNewtonMeterToNewtonMeter()
    {
        // Units: NewtonMeter=0, KiloNewtonMeter=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.torque)), 2f, 1, 0);
        Assert.That(result, Is.EqualTo(2000f).Within(1e-3f));
    }

    [Test]
    public void Velocity_KilometerPerHourToMeterPerSecound()
    {
        // Units: MeterPerSecound=0, KilometerPerHour=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.velocity)), 36f, 1, 0);
        Assert.That(result, Is.EqualTo(10f).Within(1e-3f));
    }

    [Test]
    public void Angle_DegreeToRadian()
    {
        // Units: Degree=0, Radian=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.angle)), 180f, 0, 1);
        Assert.That(result, Is.EqualTo(Mathf.PI).Within(1e-3f));
    }

    [Test]
    public void Area_SquareKilometerToSquareMeter()
    {
        // Units: SquareKilometer=0, SquareMeter=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.area)), 1f, 0, 1);
        Assert.That(result, Is.EqualTo(1_000_000f).Within(1f));
    }

    [Test]
    public void Volume_CubicKilometerToCubicMeter()
    {
        // Units: CubicKilometer=0, CubicMeter=1
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.volume)), 1f, 0, 1);
        Assert.That(result, Is.EqualTo(1_000_000_000f).Within(1f));
    }

    // Die folgenden Quantities haben aktuell nur eine einzige Einheit - eine echte Umrechnung
    // laesst sich damit nicht pruefen. Der Identitaets-Fall deckt trotzdem ab, dass die
    // Reflection (GetNestedType("Units"), GetMethod("Convert", ...)) fuer jeden Typ funktioniert.

    [Test]
    public void Impulse_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.impulse)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void Acceleration_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.acceleration)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void Jerk_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.jerk)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void HeatFlux_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.heatFlux)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void HeatTransferCoefficient_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.heatTransferCoefficient)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void ThermalConductivity_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.thermalConductivity)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void ThermalResistance_Identity()
    {
        var result = PhysicalValueDrawer.ConvertValue(Property(nameof(TestHost.thermalResistance)), 42f, 0, 0);
        Assert.That(result, Is.EqualTo(42f));
    }
}
