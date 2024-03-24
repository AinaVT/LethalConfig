using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class BoolCheckBoxConfigItem : BaseValueConfigItem<bool>
    {
        public BoolCheckBoxConfigItem(ConfigEntry<bool> configEntry, bool requiresRestart = true) : this(configEntry,
            new BoolCheckBoxOptions { RequiresRestart = requiresRestart })
        {
        }

        public BoolCheckBoxConfigItem(ConfigEntry<bool> configEntry, BoolCheckBoxOptions options) : base(configEntry,
            options)
        {
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.BoolCheckBoxPrefab);
        }
    }
}