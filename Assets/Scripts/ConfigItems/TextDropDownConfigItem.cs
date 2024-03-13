using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class TextDropDownConfigItem : BaseValueConfigItem<string>
    {
        public List<string> Values;

        public TextDropDownConfigItem(ConfigEntry<string> configEntry) : this(configEntry, true) { }
        public TextDropDownConfigItem(ConfigEntry<string> configEntry, bool requiresRestart) : this(configEntry, new TextDropDownOptions() { RequiresRestart = requiresRestart }) { }
        public TextDropDownConfigItem(ConfigEntry<string> configEntry, TextDropDownOptions options) : base(configEntry, options) {
            var acceptableValues = configEntry.Description.AcceptableValues;

            Values = (options.HasValues ? options.Values : (acceptableValues as AcceptableValueList<string>).AcceptableValues)?.ToList();
        }

        internal override GameObject CreateGameObjectForConfig() {
            return GameObject.Instantiate(Assets.TextDropDownPrefab);
        }
    }
}
