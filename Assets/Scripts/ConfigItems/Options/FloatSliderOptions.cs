using LethalConfig.ConfigItems.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalConfig.ConfigItems.Options
{
    public class FloatSliderOptions: BaseOptions
    {
        public float Min { get; set; } = 0.0f;
        public float Max { get; set; } = 1.0f;
    }
}
