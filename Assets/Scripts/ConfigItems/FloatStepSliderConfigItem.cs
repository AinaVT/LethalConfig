using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class FloatStepSliderConfigItem : BaseValueConfigItem<float>
    {
        public FloatStepSliderConfigItem(ConfigEntry<float> configEntry) : this(configEntry, true)
        {
        }

        public FloatStepSliderConfigItem(ConfigEntry<float> configEntry, bool requiresRestart) : this(
            configEntry, GetDefaultOptions(configEntry, requiresRestart))
        {
        }

        public FloatStepSliderConfigItem(ConfigEntry<float> configEntry, FloatStepSliderOptions options) : base(
            configEntry, options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.IsMinSet
                ? options.Min
                : (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? 0;
            MaxValue = options.IsMaxSet
                ? options.Max
                : (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 1;
            Step = options.Step;
        }

        internal float MaxValue { get; private set; }
        internal float MinValue { get; private set; }
        internal float Step { get; private set; }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.FloatStepSliderPrefab);
        }

        private static FloatStepSliderOptions GetDefaultOptions(ConfigEntry<float> configEntry,
            bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new FloatStepSliderOptions
            {
                Min = (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? 0,
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 1,
                Step = 0.1f,
                RequiresRestart = requiresRestart
            };
        }
    }
}