using LethalConfig.MonoBehaviours.Managers;
using System;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours.ColorPicker
{
    internal class ConfigMenuColorPicker : MonoBehaviour
    {
        public Animator WindowAnimator;
        public TextMeshProUGUI NameText;
        public ColorPickerControl ColorPickerControl;

        private Action<string> _colorSelectedCallback;

        private static readonly int _triggerIdOpen = Animator.StringToHash("Open");
        private static readonly int _triggerIdForceClose = Animator.StringToHash("ForceClose");
        private static readonly int _triggerIdClose = Animator.StringToHash("Close");

        public void Open()
        {
            var animatorState = WindowAnimator.GetCurrentAnimatorStateInfo(0);

            if (animatorState.IsName("ColorPickerNormal") || animatorState.IsName("ColorPickerAppear"))
            {
                return;
            }

            gameObject.SetActive(true);
            WindowAnimator.SetTrigger(_triggerIdOpen);
            transform.SetAsLastSibling();
        }

        public void SetColorPickerContent(string name, string hexColor, Action<string> colorSelectedCallback)
        {
            SetNameText(name);
            ColorPickerControl.SetColor(hexColor);
            _colorSelectedCallback = colorSelectedCallback;
        }

        public void Close(bool animated = true)
        {
            var animatorState = WindowAnimator.GetCurrentAnimatorStateInfo(0);

            if (animatorState.IsName("ColorPickerClosed") || animatorState.IsName("ColorPickerDisappear"))
            {
                return;
            }

            if (!animated)
            {
                gameObject.SetActive(false);
                WindowAnimator.SetTrigger(_triggerIdForceClose);
                return;
            }

            WindowAnimator.SetTrigger(_triggerIdClose);
        }

        public void SetNameText(string name)
        {
            if (NameText == null) return;

            bool isEnabled = !string.IsNullOrWhiteSpace(name);

            NameText.gameObject.SetActive(isEnabled);
            NameText.text = name;
        }

        public void OnCanelButtonClick()
        {
            Close();
            ConfigMenuManager.Instance.menuAudio.PlayCancelSfx();
        }

        public void OnConfirmButtonClick()
        {
            _colorSelectedCallback?.Invoke(ColorPickerControl.GetHexColor());

            Close();
            ConfigMenuManager.Instance.menuAudio.PlayConfirmSfx();
        }

        public void OnCloseAnimationEnd()
        {
            gameObject.SetActive(false);
        }
    }
}
