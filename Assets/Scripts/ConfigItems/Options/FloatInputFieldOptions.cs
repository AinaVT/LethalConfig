using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalConfig.ConfigItems.Options
{
    public sealed class FloatInputFieldOptions: BaseOptions
    {
        public float Min { get; set; } = float.MinValue;
        public float Max { get; set; } = float.MaxValue;
    }
}
