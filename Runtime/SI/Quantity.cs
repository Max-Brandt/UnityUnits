public struct Quantity
{
    public Quantity(float scale = 0, float offset = 0)
    {
        this.scale = scale;
        this.offset = offset;
    }
    public float scale;
    public float offset;

    public float Convert(float Value)
    {
        return scale * Value + offset;
    }

    public float ConvertBack(float Value)
    {
        return (Value - offset) / scale;
    }
}