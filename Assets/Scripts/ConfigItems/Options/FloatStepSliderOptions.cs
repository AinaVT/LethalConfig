using LethalConfig.ConfigItems.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalConfig.ConfigItems.Options
{
    public class FloatStepSliderOptions: FloatSliderOptions
    {
        public float Step { get; set; } = 0.1f;
    }
}
