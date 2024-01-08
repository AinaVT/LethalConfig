
namespace LethalConfig.ConfigItems.Options
{
    public sealed class IntSliderOptions: BaseOptions
    {
        private int _min;
        internal bool WasMinSet;
        public int Min { get => _min; set { WasMinSet = true; _min = value; } }

        private int _max;
        internal bool WasMaxSet;
        public int Max { get => _max; set { WasMaxSet = true; _max = value; } }
    }
}
