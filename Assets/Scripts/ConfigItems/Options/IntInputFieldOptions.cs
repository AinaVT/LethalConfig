
namespace LethalConfig.ConfigItems.Options
{
    public sealed class IntInputFieldOptions: BaseOptions
    {
        public int Min { get; set; } = int.MinValue;
        public int Max { get; set; } = int.MaxValue;
    }
}
