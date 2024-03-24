using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class TextDropDownConfigItem : BaseValueConfigItem<string>
    {
        public List<string> Values;

        public TextDropDownConfigItem(ConfigEntry<string> configEntry, bool requiresRestart = true) : this(configEntry,
            new TextDropDownOptions { RequiresRestart = requiresRestart })
        {
        }

        public TextDropDownConfigItem(ConfigEntry<string> configEntry, TextDropDownOptions options) : base(configEntry,
            options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            Values = (options.HasValues
                ? options.Values
                : ((AcceptableValueList<string>)acceptableValues).AcceptableValues)?.ToList();
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.TextDropDownPrefab);
        }
    }
}