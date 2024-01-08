using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class GenericButtonConfigItem : BaseConfigItem
    {
        public GenericButtonOptions ButtonOptions => Options as GenericButtonOptions;

        public GenericButtonConfigItem(string section, string name, string description, string buttonText, GenericButtonOptions.GenericButtonHandler buttonHandler) : base(new GenericButtonOptions
        {
            Section = section,
            Name = name,
            Description = description,
            ButtonText = buttonText,
            ButtonHandler = buttonHandler,
            RequiresRestart = false
        }) { }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.GenericButtonPrefab);
        }
    }
}
