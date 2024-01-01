using LethalConfig.ConfigItems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.Mods
{
    internal class Mod
    {
        public ModInfo modInfo;

        public List<BaseConfigItem> configItems;

        internal Mod(ModInfo modInfo)
        {
            this.configItems = new List<BaseConfigItem>();
            this.modInfo = modInfo;
        }

        internal void AddConfigItem(BaseConfigItem item)
        {
            configItems.Add(item);
        }
    } 
}
