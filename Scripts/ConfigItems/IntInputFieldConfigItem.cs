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
    public sealed class IntInputFieldConfigItem : BaseValueConfigItem<int>
    {
        internal int MaxValue { get; private set; }
        internal int MinValue { get; private set; }

        public IntInputFieldConfigItem(ConfigEntry<int> configEntry) : this(configEntry, true) { }
        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, IntInputFieldOptions options) : base(configEntry, options)
        {
            MinValue = options.Min;
            MaxValue = options.Max;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Prefabs.IntInputFieldPrefab);
        }

        private static IntInputFieldOptions GetDefaultOptions(ConfigEntry<int> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new()
            {
                Min = (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? int.MinValue,
                Max = (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? int.MaxValue,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
