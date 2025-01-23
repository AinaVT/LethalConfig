using System.Collections.Generic;
using System.Linq;
using LethalConfig.ConfigItems;
using LethalConfig.Mods;
using LethalConfig.MonoBehaviours.Components;
using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigList : MonoBehaviour
    {
        public GameObject listContainerObject;
        public DescriptionBox descriptionBox;
        public GameObject searchBarObject;
        public TMP_InputField searchInputField;
        public static List<string> disabledSections = new();
        public static List<SectionButton> sectionButtons = new();

        private List<(SectionHeader, List<ModConfigController>)> _sections = new();

        private void OnEnable()
        {
            Configs.HideSearchBars.SettingChanged += (object sender, System.EventArgs e) => UpdateSearchBarVisibility();
            UpdateSearchBarVisibility();
        }

        private void OnDisable()
        {
            Configs.HideSearchBars.SettingChanged -= (object sender, System.EventArgs e) => UpdateSearchBarVisibility();
        }

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

                List<ModConfigController> items = new();

                foreach (var configItem in section)
                {
                    var controller = CreateMenuItem(configItem, thisButton);

                    if (controller != null)
                    {
                        items.Add(controller);
                    }
                }

                if (items.Any())
                {
                    _sections.Add((header.GetComponent<SectionHeader>(), items));
                }
            }

            if (searchInputField != null && !Configs.HideSearchBars.Value)
            {
                OnSearchValueChanged(searchInputField.text);
            }
        }

        private ModConfigController CreateMenuItem(BaseConfigItem configItem, SectionButton button = null)
        {
            var configItemObject = configItem.CreateGameObjectForConfig();
            var controller = configItemObject.GetComponent<ModConfigController>();
            var result = controller.SetConfigItem(configItem);
            if (!result)
            {
                DestroyImmediate(configItemObject);
                return null;
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

            return controller;
        }


        private void ClearConfigList()
        {
            foreach (Transform child in listContainerObject.transform) Destroy(child.gameObject);

            sectionButtons.Clear();
            disabledSections.Clear();
            _sections.Clear();
        }

        private void UpdateSearchBarVisibility()
        {
            if (searchBarObject == null) return;

            bool visible = !Configs.HideSearchBars.Value;

            searchBarObject.SetActive(visible);

            if (visible)
            {
                OnSearchValueChanged(searchInputField != null ? searchInputField.text : string.Empty);
            }
            else
            {
                OnSearchValueChanged(string.Empty);
            }
        }

        public void OnSearchValueChanged(string value)
        {
            foreach (var section in _sections)
            {
                bool showSection = false;

                if (string.IsNullOrWhiteSpace(value) || CanShowSection(value, section))
                {
                    showSection = true;
                }

                section.Item1.gameObject.SetActive(showSection);

                foreach (var item in section.Item2)
                {
                    item.gameObject.SetActive(showSection);
                }
            }
        }

        private bool CanShowSection(string value, (SectionHeader, List<ModConfigController>) section)
        {
            string sectionName = section.Item1.textMesh.text;

            if (sectionName.Contains(value, System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            foreach (var item in section.Item2)
            {
                string itemName = item.nameTextComponent.text;

                if (itemName.Contains(value, System.StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}