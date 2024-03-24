using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class GenericButtonConfigItem : BaseConfigItem
    {
        public GenericButtonConfigItem(string section, string name, string description, string buttonText,
            GenericButtonOptions.GenericButtonHandler buttonHandler) : base(new GenericButtonOptions
        {
            Section = section,
            Name = name,
            Description = description,
            ButtonText = buttonText,
            ButtonHandler = buttonHandler,
            RequiresRestart = false
        })
        {
        }

        public GenericButtonOptions ButtonOptions => Options as GenericButtonOptions;

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.GenericButtonPrefab);
        }

        internal override bool IsSameConfig(BaseConfigItem configItem)
        {
            if (configItem is not GenericButtonConfigItem) return false;

            var isSameMod = Owner.ModInfo.Guid == configItem.Owner.ModInfo.Guid;
            var isSameSection = Section == configItem.Section;
            var isSameKey = Name == configItem.Name;
            return isSameSection && isSameKey && isSameMod;
        }
    }
}