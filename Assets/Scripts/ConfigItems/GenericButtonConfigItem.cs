using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public class GenericButtonConfigItem : BaseConfigItem
    {
        public GenericButtonOptions ButtonOptions => Options as GenericButtonOptions;

        public GenericButtonConfigItem(GenericButtonOptions options) : base(options) { }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.EnumDropDownPrefab);
        }
    }
}
