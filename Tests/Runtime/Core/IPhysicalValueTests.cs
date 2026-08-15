using System;
using System.Collections.Generic;
using NUnit.Framework;

public class IPhysicalValueTests
{
    [Test]
    public void Convert_SameUnit_ReturnsValueUnchanged_EvenWithoutConverters()
    {
        // Wenn Quell- und Zieleinheit identisch sind, wird das Dictionary gar nicht erst
        // nachgeschlagen - das darf also selbst dann nicht fehlschlagen, wenn es leer ist.
        var emptyConverters = new Dictionary<Length.Units, Quantity>();

        var result = IPhysicalValue<Length.Units>.Convert(42f, Length.Units.Meter, Length.Units.Meter, emptyConverters);

        Assert.That(result, Is.EqualTo(42f));
    }

    [Test]
    public void Convert_UnknownSourceUnit_Throws()
    {
        var converters = new Dictionary<Length.Units, Quantity>
        {
            { Length.Units.Meter, new Quantity(1f, 0f) },
        };

        Assert.Throws<Exception>(() =>
            IPhysicalValue<Length.Units>.Convert(1f, Length.Units.Kilometer, Length.Units.Meter, converters));
    }

    [Test]
    public void Convert_UnknownTargetUnit_Throws()
    {
        var converters = new Dictionary<Length.Units, Quantity>
        {
            { Length.Units.Meter, new Quantity(1f, 0f) },
        };

        Assert.Throws<Exception>(() =>
            IPhysicalValue<Length.Units>.Convert(1f, Length.Units.Meter, Length.Units.Kilometer, converters));
    }

    [Test]
    public void Convert_KnownUnits_AppliesScaleBetweenThem()
    {
        var converters = new Dictionary<Length.Units, Quantity>
        {
            { Length.Units.Meter, new Quantity(1f, 0f) },
            { Length.Units.Kilometer, new Quantity(1000f, 0f) },
        };

        var result = IPhysicalValue<Length.Units>.Convert(2f, Length.Units.Kilometer, Length.Units.Meter, converters);

        Assert.That(result, Is.EqualTo(2000f).Within(1e-3f));
    }
}
