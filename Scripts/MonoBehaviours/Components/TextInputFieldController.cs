using LethalConfig.ConfigItems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class TextInputFieldController : ModConfigController<TextInputFieldConfigItem, string>
    {
        public TextMeshProUGUI nameTextComponent;
        public TMP_InputField textInputField;

        protected override void OnSetConfigItem()
        {
            textInputField.text = ConfigItem.CurrentValue;
            UpdateAppearance();
        }

        public void OnInputFieldEndEdit(string value)
        {
            ConfigItem.CurrentValue = value;
            UpdateAppearance();
            audioManager.PlayChangeValueSFX();
        }

        public override void UpdateAppearance()
        {
            nameTextComponent.text = $"{(ConfigItem.HasValueChanged ? "*" : "")}{ConfigItem.Name}";
            textInputField.SetTextWithoutNotify(ConfigItem.CurrentValue);
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;
        }
    } 
}
