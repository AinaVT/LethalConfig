using LethalConfig.Mods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours
{
    internal class ModListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public TextMeshProUGUI textMesh;
        public GameObject selectedBorder;
        public Image modIcon;

        public delegate void OnHoverHandler();
        public event OnHoverHandler OnHoverEnter;
        public event OnHoverHandler OnHoverExit;

        internal ConfigMenuAudioManager audioManager;

        private Mod _mod;
        internal Mod mod
        {
            get
            {
                return _mod;
            }
            set
            {
                _mod = value;
                textMesh.text = _mod.modInfo.Name;
                modIcon.sprite = _mod.modInfo.Icon;
            }
        }

        internal delegate void ModSelectedHandler(Mod mod);
        internal event ModSelectedHandler modSelected;

        public void OnClick()
        {
            modSelected(_mod);
        }

        public void SetSelected(bool selected)
        {
            selectedBorder.SetActive(selected);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter();
            audioManager.PlayHoverSFX();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit();
        }

        public string GetDescription()
        {
            return $"{_mod.modInfo.Name}\n{_mod.modInfo.GUID}\n{_mod.modInfo.Version}";
        }
    } 
}
