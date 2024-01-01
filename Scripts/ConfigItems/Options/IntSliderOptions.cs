using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalConfig.ConfigItems.Options
{
    public sealed class IntSliderOptions: BaseOptions
    {
        public int Min { get; set; } = 0;
        public int Max { get; set; } = 100;
    }
}
