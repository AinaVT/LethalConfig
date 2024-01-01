using LethalConfig.Mods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LethalConfig.MonoBehaviours
{
    internal class ModList : MonoBehaviour
    {
        public GameObject modItemPrefab;
        public GameObject listContainerObject;

        public ConfigList configList;
        public DescriptionBox descriptionBox;
        public ConfigMenuAudioManager audioManager;

        private List<ModListItem> _items;

        private void Awake()
        {
            _items = new List<ModListItem>();
            audioManager = GameObject.Find("ConfigMenuAudioManager").GetComponent<ConfigMenuAudioManager>();
            BuildModList();
        }

        private void BuildModList()
        {
            _items.Clear();
            foreach (Transform child in listContainerObject.transform)
            {
                Destroy(child.gameObject);
            }

            var mods = LethalConfigManager.Mods;

            foreach (var mod in mods.Values)
            {
                var modItem = Instantiate(modItemPrefab, listContainerObject.transform);
                modItem.transform.localScale = Vector3.one;
                modItem.transform.localPosition = Vector3.zero;
                modItem.transform.localRotation = Quaternion.identity;
                var listItem = modItem.GetComponent<ModListItem>();
                listItem.mod = mod;
                listItem.modSelected += ModSelected;
                listItem.audioManager = audioManager;
                listItem.OnHoverEnter += () =>
                {
                    descriptionBox.SetDescription(listItem.GetDescription());
                };
                listItem.OnHoverExit += () =>
                {
                    descriptionBox.SetDescription("");
                };
                _items.Add(modItem.GetComponent<ModListItem>());
            }
        }

        private void ModSelected(Mod mod)
        {
            audioManager.PlayConfirmSFX();
            configList.LoadConfigsForMod(mod);

            _items.First(i => i.mod == mod).SetSelected(true);

            foreach (var item in _items.Where(i => i.mod != mod))
                item.SetSelected(false);
        }
    } 
}
