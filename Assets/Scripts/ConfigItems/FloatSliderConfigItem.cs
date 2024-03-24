using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class FloatSliderConfigItem : BaseValueConfigItem<float>
    {
        public FloatSliderConfigItem(ConfigEntry<float> configEntry, bool requiresRestart = true) : this(configEntry,
            GetDefaultOptions(configEntry, requiresRestart))
        {
        }

        public FloatSliderConfigItem(ConfigEntry<float> configEntry, FloatSliderOptions options) : base(configEntry,
            options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.IsMinSet
                ? options.Min
                : (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? 0;
            MaxValue = options.IsMaxSet
                ? options.Max
                : (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 1;
        }

        internal float MaxValue { get; private set; }
        internal float MinValue { get; private set; }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.FloatSliderPrefab);
        }

        private static FloatSliderOptions GetDefaultOptions(ConfigEntry<float> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new FloatSliderOptions
            {
                Min = (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? 0,
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 1,
                RequiresRestart = requiresRestart
            };
        }
    }
}