using BepInEx.Bootstrap;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.Mods
{
    internal class ModInfo
    {
        internal string Name { get; set; }
        internal string Guid { get; set; }
        internal string Version { get; set; }
        internal string Description { get; set; } = "";
        internal Sprite Icon { get; set; } = Assets.DefaultModIcon;

        public override string ToString()
        {
            return $"<b>{Name}</b>\n{Guid}\nv{Version}\n\n{Description}";
        }

        /// <summary>
        ///     Convenience method for getting the BepInEx <see cref="BepInEx.PluginInfo" /> of a mod from the <see cref="Guid" />
        /// </summary>
        /// <param name="pluginInfo">
        ///     If <see cref="Guid" /> is valid and was found in the Chainloader
        ///     <see cref="BepInEx.Bootstrap.Chainloader.PluginInfos" /> Dictionary,
        ///     Get set to the BepInEx <see cref="BepInEx.PluginInfo" /> of the corresponding mod.
        ///     Otherwise it gets set to null
        /// </param>
        /// <returns>
        ///     true if the <see cref="Guid" /> is valid and was found in the Chainloader
        ///     <see cref="BepInEx.Bootstrap.Chainloader.PluginInfos" /> Dictionary,
        ///     Otherwise returns false
        /// </returns>
        public bool TryGetPluginInfo(out BepInEx.PluginInfo pluginInfo)
        {
            return Chainloader.PluginInfos.TryGetValue(Guid, out pluginInfo);
        }
    }
}