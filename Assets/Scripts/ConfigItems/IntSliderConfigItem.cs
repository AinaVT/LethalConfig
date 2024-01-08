using BepInEx.Configuration;
using UnityEngine;
using LethalConfig.Utils;
using LethalConfig.ConfigItems.Options;
using System;

namespace LethalConfig.ConfigItems
{
    public sealed class IntSliderConfigItem : BaseValueConfigItem<int>
    {
        internal int MaxValue { get; private set; }
        internal int MinValue { get; private set; }

        public IntSliderConfigItem(ConfigEntry<int> configEntry) : this(configEntry, true) { }
        public IntSliderConfigItem(ConfigEntry<int> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public IntSliderConfigItem(ConfigEntry<int> configEntry, IntSliderOptions options) : base(configEntry, options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.WasMinSet ? options.Min : (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0; 
            MaxValue = options.WasMaxSet ? options.Max : (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.IntSliderPrefab);
        }

        private static IntSliderOptions GetDefaultOptions(ConfigEntry<int> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new()
            {
                Min = (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0,
                Max = (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
