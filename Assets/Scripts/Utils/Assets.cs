using UnityEngine;

namespace LethalConfig.Utils
{
    internal static class Assets
    {
        private static AssetBundle _assetBundle;

        internal static GameObject ConfigMenuManagerPrefab;
        internal static GameObject ConfigMenuNotificationPrefab;
        internal static GameObject ConfigMenuPrefab;
        internal static GameObject ModListItemPrefab;
        internal static GameObject SectionHeaderPrefab;
        internal static GameObject IntSliderPrefab;
        internal static GameObject FloatSliderPrefab;
        internal static GameObject FloatStepSliderPrefab;
        internal static GameObject BoolCheckBoxPrefab;
        internal static GameObject EnumDropDownPrefab;
        internal static GameObject TextDropDownPrefab;
        internal static GameObject TextInputFieldPrefab;
        internal static GameObject IntInputFieldPrefab;
        internal static GameObject FloatInputFieldPrefab;
        internal static GameObject GenericButtonPrefab;

        internal static Sprite DefaultModIcon;
        internal static Sprite LethalConfigModIcon;

        internal static void Init()
        {
            _assetBundle = AssetBundle.LoadFromFile(PathUtils.PathForResourceInAssembly("ainavt_lethalconfig"));
            if (_assetBundle == null)
            {
                LogUtils.LogError("Failed to load LethalConfig bundle.");
            }
            else
            {
                // UI Prefabs
                LoadAsset("prefabs/ConfigMenuManager.prefab", out ConfigMenuManagerPrefab);
                LoadAsset("prefabs/ConfigMenuNotification.prefab", out ConfigMenuNotificationPrefab);
                LoadAsset("prefabs/ConfigMenu.prefab", out ConfigMenuPrefab);
                LoadAsset("prefabs/ModListItem.prefab", out ModListItemPrefab);
                if(Configs.AddSectionButtons.Value) //swap out prefabs based on config item
                    LoadAsset("prefabs/components/SectionHeader WithButton.prefab", out SectionHeaderPrefab);
                else
                    LoadAsset("prefabs/components/SectionHeader Old.prefab", out SectionHeaderPrefab);
                LoadAsset("prefabs/components/IntSliderItem.prefab", out IntSliderPrefab);
                LoadAsset("prefabs/components/FloatSliderItem.prefab", out FloatSliderPrefab);
                LoadAsset("prefabs/components/FloatStepSliderItem.prefab", out FloatStepSliderPrefab);
                LoadAsset("prefabs/components/BoolCheckBoxItem.prefab", out BoolCheckBoxPrefab);
                LoadAsset("prefabs/components/EnumDropDownItem.prefab", out EnumDropDownPrefab);
                LoadAsset("prefabs/components/TextDropDownItem.prefab", out TextDropDownPrefab);
                LoadAsset("prefabs/components/TextInputFieldItem.prefab", out TextInputFieldPrefab);
                LoadAsset("prefabs/components/IntInputFieldItem.prefab", out IntInputFieldPrefab);
                LoadAsset("prefabs/components/FloatInputFieldItem.prefab", out FloatInputFieldPrefab);
                LoadAsset("prefabs/components/GenericButtonItem.prefab", out GenericButtonPrefab);

                // Icons
                LoadAsset("sprite/unknown-icon.png", out DefaultModIcon);
                LoadAsset("icon.png", out LethalConfigModIcon);

                LogUtils.LogDebug("Finished loading assets.");
            }
        }

        private static void LoadAsset<T>(string assetName, out T asset) where T : Object
        {
            var assetPath = $"Assets/{assetName}";
            asset = _assetBundle.LoadAsset<T>(assetPath);
            if (asset == null) LogUtils.LogError($"Failed to load asset ({assetPath})");
        }
    }
}