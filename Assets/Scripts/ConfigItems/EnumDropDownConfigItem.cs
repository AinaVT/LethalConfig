using System;
using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LethalConfig.ConfigItems
{
    public class EnumDropDownConfigItem<T> : BaseValueConfigItem<T> where T : Enum
    {
        public EnumDropDownConfigItem(ConfigEntry<T> configEntry, bool requiresRestart = true) : this(configEntry,
            new EnumDropDownOptions { RequiresRestart = requiresRestart })
        {
        }

        public EnumDropDownConfigItem(ConfigEntry<T> configEntry, EnumDropDownOptions options) : base(configEntry,
            options)
        {
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.EnumDropDownPrefab);
        }
    }
}