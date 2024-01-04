using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.Mods
{
    internal class ModInfo
    {
        internal string Name { get; set; }
        internal string GUID { get; set; }
        internal string Version { get; set; }
        internal string Description { get; set; }
        internal Sprite Icon { get; set; } = Assets.DefaultModIcon;

        public override string ToString()
        {
            return $"<b>{Name}</b>\n{GUID}\nv{Version}\n\n{Description}";
        }
    } 
}
