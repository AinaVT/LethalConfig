using LethalConfig.ConfigItems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class FloatInputFieldController : ModConfigController<FloatInputFieldConfigItem, float>
    {
        public TMP_InputField textInputField;

        public override string GetDescription()
        {
            var description = base.GetDescription();
            if (ConfigItem.MinValue != float.MinValue)
            {
                description += $"\nMin: {ConfigItem.MinValue:0.0#}";
            }
            if (ConfigItem.MaxValue != float.MaxValue)
            {
                description += $"\nMax: {ConfigItem.MaxValue:0.0#}";
            }

            return description;
        }

        protected override void OnSetConfigItem()
        {
            textInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue}");
            UpdateAppearance();
        }

        public void OnInputFieldEndEdit(string value)
        {
            if (float.TryParse(value, out var newValue))
            {
                ConfigItem.CurrentValue = Math.Clamp(newValue, ConfigItem.MinValue, ConfigItem.MaxValue);
            }
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSFX();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            textInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue}");
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;
        }
    } 
}
