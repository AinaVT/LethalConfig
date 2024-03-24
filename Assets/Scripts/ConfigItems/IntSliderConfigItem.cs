using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LethalConfig.ConfigItems
{
    public sealed class IntSliderConfigItem : BaseValueConfigItem<int>
    {
        public IntSliderConfigItem(ConfigEntry<int> configEntry, bool requiresRestart = true) : this(configEntry,
            GetDefaultOptions(configEntry, requiresRestart))
        {
        }

        public IntSliderConfigItem(ConfigEntry<int> configEntry, IntSliderOptions options) : base(configEntry, options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.IsMinSet ? options.Min : (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0;
            MaxValue = options.IsMaxSet
                ? options.Max
                : (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100;
        }

        internal int MaxValue { get; private set; }
        internal int MinValue { get; private set; }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.IntSliderPrefab);
        }

        private static IntSliderOptions GetDefaultOptions(ConfigEntry<int> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new IntSliderOptions
            {
                Min = (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0,
                Max = (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100,
                RequiresRestart = requiresRestart
            };
        }
    }
}