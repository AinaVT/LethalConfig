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
        public TMP_InputField textInputField;

        protected override void OnSetConfigItem()
        {
            textInputField.SetTextWithoutNotify(ConfigItem.CurrentValue);
            UpdateAppearance();
        }

        public void OnInputFieldEndEdit(string value)
        {
            ConfigItem.CurrentValue = value;
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSFX();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            textInputField.SetTextWithoutNotify(ConfigItem.CurrentValue);
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;
        }
    } 
}
