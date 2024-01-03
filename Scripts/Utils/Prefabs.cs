using SharedUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LethalConfig.Utils
{
    internal static class Prefabs
    {
        private static AssetBundle assetBundle;

        internal static GameObject ConfigMenuPrefab;
        internal static GameObject ModListItemPrefab;
        internal static GameObject SectionHeaderPrefab;
        internal static GameObject IntSliderPrefab;
        internal static GameObject FloatSliderPrefab;
        internal static GameObject FloatStepSliderPrefab;
        internal static GameObject BoolCheckBoxPrefab;
        internal static GameObject EnumDropDownPrefab;
        internal static GameObject TextInputFieldPrefab;
        internal static GameObject IntInputFieldPrefab;
        internal static GameObject FloatInputFieldPrefab;

        internal static void Init()
        {
            assetBundle = AssetBundle.LoadFromFile(PathUtils.PathForResourceInAssembly("ainavt_lethalconfig"));

            if (assetBundle == null)
            {
                LogUtils.LogError("Failed to load LethalConfig bundle.");
            }
            else
            {
                LoadPrefab("ConfigMenu.prefab", out ConfigMenuPrefab);
                LoadPrefab("ModListItem.prefab", out ModListItemPrefab);
                LoadPrefab("SectionHeader.prefab", out SectionHeaderPrefab);
                LoadPrefab("IntSliderItem.prefab", out IntSliderPrefab);
                LoadPrefab("FloatSliderItem.prefab", out FloatSliderPrefab);
                LoadPrefab("FloatStepSliderItem.prefab", out FloatStepSliderPrefab);
                LoadPrefab("BoolCheckBoxItem.prefab", out BoolCheckBoxPrefab);
                LoadPrefab("EnumDropDownItem.prefab", out EnumDropDownPrefab);
                LoadPrefab("TextInputFieldItem.prefab", out TextInputFieldPrefab);
                LoadPrefab("IntInputFieldItem.prefab", out IntInputFieldPrefab);
                LoadPrefab("FloatInputFieldItem.prefab", out FloatInputFieldPrefab);

                LogUtils.LogInfo("Finished loading prefabs.");
            }
        }

        private static void LoadPrefab(string prefabName, out GameObject prefab)
        {
            var prefabPath = $"Assets/mods/lethalconfig/prefabs/{prefabName}";
            prefab = assetBundle.LoadAsset<GameObject>(prefabPath);
            if (prefab == null)
            {
                LogUtils.LogError($"Failed to load prefab ({prefabPath})");
            }
        }
    } 
}
