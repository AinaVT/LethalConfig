using BepInEx.Configuration;
using UnityEngine;
using LethalConfig.Utils;
using LethalConfig.ConfigItems.Options;

namespace LethalConfig.ConfigItems
{
    public sealed class FloatSliderConfigItem : BaseValueConfigItem<float>
    {
        internal float MaxValue { get; private set; }
        internal float MinValue { get; private set; }

        public FloatSliderConfigItem(ConfigEntry<float> configEntry) : this(configEntry, true) { }
        public FloatSliderConfigItem(ConfigEntry<float> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public FloatSliderConfigItem(ConfigEntry<float> configEntry, FloatSliderOptions options) : base(configEntry, options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.WasMinSet ? options.Min : (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0;
            MaxValue = options.WasMaxSet ? options.Max : (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 1;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.FloatSliderPrefab);
        }

        private static FloatSliderOptions GetDefaultOptions(ConfigEntry<float> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new()
            {
                Min = (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? 0,
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 1,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
