using BepInEx.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LethalConfig.Utils;
using UnityEngine.EventSystems;
using LethalConfig.ConfigItems.Options;
using DigitalRuby.ThunderAndLightning;

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
            MinValue = options.Min;
            MaxValue = options.Max;
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
