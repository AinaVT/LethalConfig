using LethalConfig.Mods;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours
{
    internal class ModListItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public delegate void OnHoverHandler();

        public TextMeshProUGUI textMesh;
        public GameObject selectedBorder;
        public Image modIcon;

        private Mod _mod;

        internal Mod Mod
        {
            get => _mod;
            set
            {
                _mod = value;
                textMesh.text = _mod.ModInfo.Name;
                modIcon.sprite = _mod.ModInfo.Icon;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit?.Invoke();
        }

        public event OnHoverHandler OnHoverEnter;
        public event OnHoverHandler OnHoverExit;
        internal event ModSelectedHandler ModSelected;

        public void OnClick()
        {
            ModSelected?.Invoke(_mod);
        }

        public void SetSelected(bool selected)
        {
            selectedBorder.SetActive(selected);
        }

        public string GetDescription()
        {
            return $"{_mod.ModInfo.Name}\n{_mod.ModInfo.Guid}\n{_mod.ModInfo.Version}";
        }

        internal delegate void ModSelectedHandler(Mod mod);
    }
}