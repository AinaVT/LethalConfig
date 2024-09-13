using System.Collections.Generic;
using System.Linq;
using LethalConfig.ConfigItems;
using LethalConfig.Mods;
using LethalConfig.MonoBehaviours.Components;
using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigList : MonoBehaviour
    {
        public GameObject listContainerObject;
        public DescriptionBox descriptionBox;
        public static List<string> disabledSections = new();
        public static List<SectionButton> sectionButtons = new();

        internal void LoadConfigsForMod(Mod mod)
        {
            ClearConfigList();

            var sections = mod.ConfigItems.GroupBy(c => c.Section);

            foreach (var section in sections)
            {
                var header = Instantiate(Assets.SectionHeaderPrefab, listContainerObject.transform, true);
                header.GetComponent<SectionHeader>().SetSectionName(section.Key);
                header.GetComponent<SectionHeader>().SetSectionButton(section, out SectionButton thisButton);
                header.transform.localPosition = Vector3.zero;
                header.transform.localScale = Vector3.one;
                header.transform.localRotation = Quaternion.identity;

                if(thisButton != null)
                    sectionButtons.Add(thisButton);

                foreach (var configItem in section)
                {
                    CreateMenuItem(configItem, thisButton);
                }
            }
        }

        private void CreateMenuItem(BaseConfigItem configItem, SectionButton button = null)
        {
            var configItemObject = configItem.CreateGameObjectForConfig();
            var controller = configItemObject.GetComponent<ModConfigController>();
            var result = controller.SetConfigItem(configItem);
            if (!result)
            {
                DestroyImmediate(configItemObject);
                return;
            }

            configItemObject.transform.SetParent(listContainerObject.transform);
            configItemObject.transform.localPosition = Vector3.zero;
            configItemObject.transform.localScale = Vector3.one;
            configItemObject.transform.localRotation = Quaternion.identity;
            controller.OnHoverEnter += () =>
            {
                descriptionBox.ShowConfigInfo(controller.GetDescription());
                ConfigMenuManager.Instance.menuAudio.PlayHoverSfx();
            };

            if(button != null)
            {
                if (Configs.SectionsDefaultClosed.Value)
                {
                    if(!disabledSections.Contains(button.sectionName))
                        disabledSections.Add(button.sectionName);

                    configItemObject.SetActive(false);
                    button.buttonText.text = "Show";
                }
                    
                button.AddTransform(configItemObject.transform);
            }
            
        }


        private void ClearConfigList()
        {
            foreach (Transform child in listContainerObject.transform) Destroy(child.gameObject);

            sectionButtons.Clear();
            disabledSections.Clear();
        }
    }
}