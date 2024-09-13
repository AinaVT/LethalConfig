using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours
{
    internal class SectionHeader : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;
        public Button sectionButton;
        public TextMeshProUGUI buttonTextMesh;

        public void SetSectionName(string sectionName)
        {
            textMesh.text = $"[{sectionName}]";
        }

        public void SetSectionButton(IGrouping<string, BaseConfigItem> section, out SectionButton thisButton)
        {
            if(!Configs.AddSectionButtons.Value)
            {
                thisButton = null;
                return;
            }

            sectionButton.onClick.RemoveAllListeners();
            sectionButton.onClick = new Button.ButtonClickedEvent();

            sectionButton.onClick.AddListener(delegate
            {
                ConfigMenuManager.Instance.menuAudio.PlayConfirmSfx();
                OnToggle(section);
            });

            buttonTextMesh.text = "Hide";
            thisButton = new(section.Key, section.ToList(), buttonTextMesh);
        }

        private void OnToggle(IGrouping<string, BaseConfigItem> section)
        {
            if (!ConfigList.disabledSections.Contains(section.Key))
                ToggleSection(false, section);
            else
                ToggleSection(true, section);
        }


        private void ToggleSection(bool state, IGrouping<string, BaseConfigItem> section)
        {
            int itemsAdjusted = 0;
            if (!state)
            {
                foreach (SectionButton child in ConfigList.sectionButtons)
                {
                    if (child.sectionName == section.Key)
                    {
                        foreach (Transform item in child.transformsInSection)
                        {
                            itemsAdjusted++;
                            item.gameObject.SetActive(false);
                            buttonTextMesh.text = "Show";
                        }
                    }
                }
                LogUtils.LogDebug($"{itemsAdjusted} config item(s) hidden in section: {section.Key}");
                ConfigList.disabledSections.Add(section.Key);
            }
            else
            {
                foreach (SectionButton child in ConfigList.sectionButtons)
                {
                    if (child.sectionName == section.Key)
                    {
                        foreach (Transform item in child.transformsInSection)
                        {
                            itemsAdjusted++;
                            item.gameObject.SetActive(true);
                            buttonTextMesh.text = "Hide";
                        }
                    }
                }

                LogUtils.LogDebug($"{itemsAdjusted} config item(s) shown in section: {section.Key}");
                ConfigList.disabledSections.Remove(section.Key);
            }
        }
    }


    internal class SectionButton
    {
        internal string sectionName = "";
        internal List<BaseConfigItem> itemsInSection = new();
        internal List<Transform> transformsInSection = new();
        internal TextMeshProUGUI buttonText;

        internal SectionButton(string sectionName, List<BaseConfigItem> itemsInSection, TextMeshProUGUI button)
        {
            this.sectionName = sectionName;
            this.itemsInSection = itemsInSection;
            this.buttonText = button;

        }

        internal void AddTransform(Transform item)
        {
            if (!this.transformsInSection.Contains(item))
                this.transformsInSection.Add(item);
        }

    }
}