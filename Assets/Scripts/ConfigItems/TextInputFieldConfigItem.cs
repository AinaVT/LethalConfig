using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class TextInputFieldConfigItem : BaseValueConfigItem<string>
    {
        public TextInputFieldConfigItem(ConfigEntry<string> configEntry) : this(configEntry, true) 
        { 
        }

        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, bool requiresRestart = true) : this(
            configEntry, GetDefaultOptions(requiresRestart))
        {
        }

        public TextInputFieldConfigItem(ConfigEntry<string> configEntry, TextInputFieldOptions options) : base(
            configEntry, options)
        {
            CharacterLimit = options.CharacterLimit;
            NumberOfLines = options.NumberOfLines;
            TrimText = options.TrimText;
        }

        internal int CharacterLimit { get; private set; }
        internal int NumberOfLines { get; private set; }
        internal bool TrimText { get; private set; }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.TextInputFieldPrefab);
        }

        private static TextInputFieldOptions GetDefaultOptions(bool requiresRestart = true)
        {
            return new TextInputFieldOptions
            {
                CharacterLimit = 0,
                NumberOfLines = 1,
                RequiresRestart = requiresRestart
            };
        }
    }
}