using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class FloatInputFieldConfigItem : BaseValueConfigItem<float>
    {
        public FloatInputFieldConfigItem(ConfigEntry<float> configEntry, bool requiresRestart = true) : this(
            configEntry, GetDefaultOptions(configEntry, requiresRestart))
        {
        }

        public FloatInputFieldConfigItem(ConfigEntry<float> configEntry, FloatInputFieldOptions options) : base(
            configEntry, options)
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
            return Object.Instantiate(Assets.FloatInputFieldPrefab);
        }

        private static FloatInputFieldOptions GetDefaultOptions(ConfigEntry<float> configEntry,
            bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new FloatInputFieldOptions
            {
                Min = (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? float.MinValue,
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? float.MaxValue,
                RequiresRestart = requiresRestart
            };
        }
    }
}