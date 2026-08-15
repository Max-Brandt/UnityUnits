using NUnit.Framework;

public class QuantityTests
{
    [Test]
    public void Convert_AppliesScaleAndOffset()
    {
        var q = new Quantity(scale: 2f, offset: 5f);

        Assert.That(q.Convert(10f), Is.EqualTo(25f).Within(1e-6f));
    }

    [Test]
    public void ConvertBack_AppliesInverseOfConvert()
    {
        var q = new Quantity(scale: 2f, offset: 5f);

        Assert.That(q.ConvertBack(25f), Is.EqualTo(10f).Within(1e-6f));
    }

    [TestCase(1f, 0f, 42f)]
    [TestCase(2f, 5f, -3f)]
    [TestCase(0.5f, -10f, 100f)]
    public void ConvertBack_IsInverseOfConvert_ForRandomValues(float scale, float offset, float value)
    {
        var q = new Quantity(scale, offset);

        var converted = q.Convert(value);
        var roundTripped = q.ConvertBack(converted);

        Assert.That(roundTripped, Is.EqualTo(value).Within(1e-4f));
    }

    [Test]
    public void DefaultConstructor_UsesZeroScaleAndOffset()
    {
        var q = new Quantity();

        Assert.That(q.scale, Is.EqualTo(0f));
        Assert.That(q.offset, Is.EqualTo(0f));
    }
}
