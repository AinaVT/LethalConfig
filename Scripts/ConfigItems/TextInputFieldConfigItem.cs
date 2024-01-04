using BepInEx.Configuration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LethalConfig.Utils;
using UnityEngine.EventSystems;
using LethalConfig.ConfigItems.Options;

namespace LethalConfig.ConfigItems
{
    public sealed class TextInputFieldConfigItem : BaseValueConfigItem<string>
    {
        internal int CharacterLimit { get; private set; }

        public TextInputFieldConfigItem(ConfigEntry<string> configEntry) : this(configEntry, true) { }
        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, TextInputFieldOptions options) : base(configEntry, options)
        {
            CharacterLimit = options.CharacterLimit;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.TextInputFieldPrefab);
        }

        private static TextInputFieldOptions GetDefaultOptions(ConfigEntry<string> configEntry, bool requiresRestart = true)
        {
            return new()
            {
                CharacterLimit = 0,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
