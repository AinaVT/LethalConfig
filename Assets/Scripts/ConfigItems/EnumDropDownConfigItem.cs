using BepInEx.Configuration;
using LethalConfig.Utils;
using System;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class EnumDropDownConfigItem<T> : BaseValueConfigItem<T> where T: Enum
    {
        public EnumDropDownConfigItem(ConfigEntry<T> configEntry, bool requiresRestart = true) : base(configEntry, new() { RequiresRestart = requiresRestart })
        {
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.EnumDropDownPrefab);
        }
    }
}
