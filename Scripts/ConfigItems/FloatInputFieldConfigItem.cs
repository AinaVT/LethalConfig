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
    public sealed class FloatInputFieldConfigItem : BaseValueConfigItem<float>
    {
        internal float MaxValue { get; private set; }
        internal float MinValue { get; private set; }

        public FloatInputFieldConfigItem(ConfigEntry<float> configEntry) : this(configEntry, true) { }
        public FloatInputFieldConfigItem(ConfigEntry<float> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public FloatInputFieldConfigItem(ConfigEntry<float> configEntry, FloatInputFieldOptions options) : base(configEntry, options)
        {
            MinValue = options.Min;
            MaxValue = options.Max;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.FloatInputFieldPrefab);
        }

        private static FloatInputFieldOptions GetDefaultOptions(ConfigEntry<float> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new()
            {
                Min = (acceptableValues as AcceptableValueRange<float>)?.MinValue ?? float.MinValue,
                Max = (acceptableValues as AcceptableValueRange<float>)?.MaxValue ?? float.MaxValue,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
