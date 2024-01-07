
namespace LethalConfig.ConfigItems.Options
{
    public sealed class FloatInputFieldOptions: BaseOptions
    {
        public float Min { get; set; } = float.MinValue;
        public float Max { get; set; } = float.MaxValue;
    }
}
