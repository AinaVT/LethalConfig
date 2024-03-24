using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LethalConfig.AutoConfig;
using LethalConfig.ConfigItems;
using Newtonsoft.Json;
using UnityEngine;

namespace LethalConfig.Mods
{
    internal class Mod
    {
        public readonly List<BaseConfigItem> ConfigItems;
        public readonly List<ConfigEntryPath> EntriesToSkipAutoGen;
        public readonly ModInfo ModInfo;
        public bool IsAutoGenerated = false;

        internal Mod(ModInfo modInfo)
        {
            ConfigItems = new List<BaseConfigItem>();
            EntriesToSkipAutoGen = new List<ConfigEntryPath>();
            ModInfo = modInfo;

            // Auto detect icon and description before mods override them.
            TryResolveIconAndDesc();
        }

        private void TryResolveIconAndDesc()
        {
            if (!ModInfo.TryGetPluginInfo(out var pluginInfo))
                return;

            var searchDir = Path.GetFullPath(pluginInfo.Location);
            var parent = Directory.GetParent(searchDir);

            // Ensure our search directory has a parent of `plugins` ex: `plugins/SEARCH_DIR`, this should leave us with a best match for how r2modman and it's derivatives' install mods.
            while (parent is not null && !string.Equals(parent.Name, "plugins", StringComparison.OrdinalIgnoreCase))
            {
                searchDir = parent.FullName;
                parent = Directory
                    .GetParent(searchDir); // This prevents an infinite loop, as parent becomes null if we hit the root of the drive.
            }

            if (searchDir.EndsWith(
                    ".dll")) // Return early if the searchDir is a dll file, prevents a crash from occuring below. Commonly occurs when manually installing mods.
                return;

            var iconPath = Directory.EnumerateFiles(searchDir, "icon.png", SearchOption.AllDirectories)
                .FirstOrDefault();
            LoadIcon(iconPath);

            var manifestPath = Directory.EnumerateFiles(searchDir, "manifest.json", SearchOption.AllDirectories)
                .FirstOrDefault();
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

                ModInfo.Icon = Sprite.Create(iconTex, new Rect(0, 0, iconTex.width, iconTex.height),
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

                ModInfo.Description = manifest.Description;
            }
            catch
            {
                // Catch-all just to make sure we don't crash if we can't load the manifest.
            }
        }

        internal void AddConfigItem(BaseConfigItem item)
        {
            ConfigItems.Add(item);
        }
    }
}