using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using System;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class EnumDropDownConfigItem<T> : BaseValueConfigItem<T> where T: Enum
    {
        public EnumDropDownConfigItem(ConfigEntry<T> configEntry) : this(configEntry, true) { }
        public EnumDropDownConfigItem(ConfigEntry<T> configEntry, bool requiresRestart) : this(configEntry, new EnumDropDownOptions() { RequiresRestart = requiresRestart }) { }
        public EnumDropDownConfigItem(ConfigEntry<T> configEntry, EnumDropDownOptions options) : base(configEntry, options) { }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.EnumDropDownPrefab);
        }
    }
}
