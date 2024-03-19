using BepInEx.Configuration;
using UnityEngine;
using LethalConfig.Utils;
using LethalConfig.ConfigItems.Options;

namespace LethalConfig.ConfigItems
{
    public sealed class TextInputFieldConfigItem : BaseValueConfigItem<string>
    {
        internal int CharacterLimit { get; private set; }
        internal int NumberOfLines { get; private set; }
        internal bool TrimText { get; private set; }

        public TextInputFieldConfigItem(ConfigEntry<string> configEntry) : this(configEntry, true) { }
        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, TextInputFieldOptions options) : base(configEntry, options)
        {
            CharacterLimit = options.CharacterLimit;
            NumberOfLines = options.NumberOfLines;
            TrimText = options.TrimText;
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
                NumberOfLines = 1,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
