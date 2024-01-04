using System;
using LethalConfig.ConfigItems;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace LethalConfig.Mods
{
    internal class Mod
    {
        public ModInfo modInfo;
        public bool IsAutoGenerated = false;

        public List<BaseConfigItem> configItems;

        internal Mod(ModInfo modInfo)
        {
            this.configItems = new List<BaseConfigItem>();
            this.modInfo = modInfo;
            
            // Auto detect icon and description before mods override them.
            TryResolveIconAndDesc();
        }

        internal void TryResolveIconAndDesc()
        {
            if (!modInfo.TryGetPluginInfo(out var pluginInfo))
                return;

            var searchDir = Path.GetFullPath(pluginInfo.Location);
            var parent = Directory.GetParent(searchDir);
            
            // Ensure our search directory has a parent of `plugins` ex: `plugins/SEARCH_DIR`, this should leave us with a best match for how r2modman and it's derivatives' install mods.
            while (parent is not null && !string.Equals(parent.Name, "plugins", StringComparison.OrdinalIgnoreCase))
            {
                searchDir = parent.FullName;
                parent = Directory.GetParent(searchDir); // This prevents an infinite loop, as parent becomes null if we hit the root of the drive.
            }

            var iconPath = Directory.EnumerateFiles(searchDir, "icon.png", SearchOption.AllDirectories).FirstOrDefault();
            LoadIcon(iconPath);

            var manifestPath = Directory.EnumerateFiles(searchDir, "manifest.json", SearchOption.AllDirectories).FirstOrDefault();
            LoadDesc(manifestPath);
        }

        private void LoadIcon(string iconPath)
        {
            try
            {
                if (iconPath == default)
                    return;

                var iconTex = new Texture2D(256, 256);
                if (!iconTex.LoadImage(File.ReadAllBytes(iconPath)))
                    return;

                modInfo.Icon = Sprite.Create(iconTex, new Rect(0, 0, iconTex.width, iconTex.height),
                    new Vector2(0.5f, 0.5f), 100);
            }
            catch
            {
                // Catch-all just to make sure we don't crash if we can't load the icon.
            }
        }

        private void LoadDesc(string manifestPath)
        {
            try
            {
                if (manifestPath == default)
                    return;

                var manifestContents = File.ReadAllText(manifestPath);
                
                var manifest = JsonConvert.DeserializeObject<ThunderstoreManifest>(manifestContents);
                if (manifest is null)
                    return;

                modInfo.Description = manifest.Description;
            }
            catch
            {
                // Catch-all just to make sure we don't crash if we can't load the manifest.
            }
        }

        internal void AddConfigItem(BaseConfigItem item)
        {
            configItems.Add(item);
        }
    }
}
