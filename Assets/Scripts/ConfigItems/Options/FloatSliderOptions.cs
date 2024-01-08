
namespace LethalConfig.ConfigItems.Options
{
    public class FloatSliderOptions: BaseOptions
    {
        private float _min;
        internal bool WasMinSet;
        public float Min { get => _min; set { WasMinSet = true; _min = value; } }

        private float _max;
        internal bool WasMaxSet;
        public float Max { get => _max; set { WasMaxSet = true; _max = value; } }
    }
}
