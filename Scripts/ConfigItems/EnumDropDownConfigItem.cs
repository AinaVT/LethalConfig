using BepInEx.Configuration;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
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
