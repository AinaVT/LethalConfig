using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class HexColorInputFieldConfigItem : BaseValueConfigItem<string>
    {
        public HexColorInputFieldConfigItem(ConfigEntry<string> configEntry) : this(configEntry, true)
        {

        }

        public HexColorInputFieldConfigItem(ConfigEntry<string> configEntry, bool requiresRestart) : this(configEntry, new HexColorInputFieldOptions { RequiresRestart = requiresRestart })
        {

        }

        public HexColorInputFieldConfigItem(ConfigEntry<string> configEntry, HexColorInputFieldOptions options) : base(configEntry, options)
        {

        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.HexColorInputFieldPrefab);
        }
    }
}
