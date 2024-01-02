using BepInEx.Configuration;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class BoolCheckBoxConfigItem : BaseValueConfigItem<bool>
    {
        public BoolCheckBoxConfigItem(ConfigEntry<bool> configEntry, bool requiresRestart = true) : base(configEntry, new() { RequiresRestart = requiresRestart })
        {
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Prefabs.BoolCheckBoxPrefab);
        }
    }
}
