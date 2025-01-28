using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class HexColorInputFieldController : ModConfigController<HexColorInputFieldConfigItem, string>
    {
        public TMP_InputField textInputField;
        public Image colorPreviewImage;

        protected override void OnSetConfigItem()
        {
            textInputField.text = ConfigItem.CurrentValue;
            
            UpdateAppearance();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();

            textInputField.text = ConfigItem.CurrentValue;
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;

            UpdateColorPreviewImage(ConfigItem.CurrentValue);
        }

        public void UpdateColorPreviewImage(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
            {
                colorPreviewImage.color = color;
            }
        }

        public void OnInputFieldValueChanged(string value)
        {
            UpdateColorPreviewImage(value);
        }

        public void OnInputFieldEndEdit(string value)
        {
            ConfigItem.CurrentValue = value.Trim();

            UpdateAppearance();

            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public void OnColorPickerButtonClick()
        {
            ConfigMenuManager.ShowColorPicker(ConfigItem.Name, ConfigItem.CurrentValue, newHexColor =>
            {
                ConfigItem.CurrentValue = newHexColor;
                UpdateAppearance();
            });
        }
    }
}
