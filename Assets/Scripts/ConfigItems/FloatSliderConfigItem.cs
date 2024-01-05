using BepInEx.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LethalConfig.Utils;
using UnityEngine.EventSystems;
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
            MinValue = options.Min;
            MaxValue = options.Max;
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
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? 100,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
