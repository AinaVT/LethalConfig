using BepInEx.Configuration;
using LethalConfig.Utils;
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
            return GameObject.Instantiate(Assets.BoolCheckBoxPrefab);
        }
    }
}
