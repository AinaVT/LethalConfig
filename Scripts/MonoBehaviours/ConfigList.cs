using LethalConfig.ConfigItems;
using LethalConfig.Mods;
using LethalConfig.MonoBehaviours.Components;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigList : MonoBehaviour
    {
        public GameObject listContainerObject;
        public DescriptionBox descriptionBox;
        public ConfigMenuAudioManager audioManager;

        internal void LoadConfigsForMod(Mod mod)
        {
            ClearConfigList();

            var sections = mod.configItems.GroupBy(c => c.Section);

            foreach (var section in sections)
            {
                var header = Instantiate(Prefabs.SectionHeaderPrefab);
                header.GetComponent<SectionHeader>().SetSectionName(section.Key);
                header.transform.SetParent(listContainerObject.transform);
                header.transform.localPosition = Vector3.zero;
                header.transform.localScale = Vector3.one;
                header.transform.localRotation = Quaternion.identity;

                foreach (var configItem in section)
                {
                    var configItemObject = configItem.CreateGameObjectForConfig();
                    var controller = configItemObject.GetComponent<ModConfigController>();
                    var result = controller.SetConfigItem(configItem);
                    if (!result)
                    {
                        DestroyImmediate(configItemObject.gameObject);
                        return;
                    }

                    configItemObject.transform.SetParent(listContainerObject.transform);
                    configItemObject.transform.localPosition = Vector3.zero;
                    configItemObject.transform.localScale = Vector3.one;
                    configItemObject.transform.localRotation = Quaternion.identity;
                    controller.OnHoverEnter += () =>
                    {
                        descriptionBox.SetDescription(controller.GetDescription());
                    };
                    controller.OnHoverExit += () =>
                    {
                        descriptionBox.SetDescription("");
                    };
                    controller.audioManager = audioManager;
                }
            }
        }

        private void ClearConfigList()
        {
            foreach (Transform child in listContainerObject.transform)
            {
                Destroy(child.gameObject);
            }
        }
    } 
}