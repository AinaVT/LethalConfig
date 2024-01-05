using LethalConfig.ConfigItems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class IntInputFieldController : ModConfigController<IntInputFieldConfigItem, int>
    {
        public TMP_InputField textInputField;

        public override string GetDescription()
        {
            var description = base.GetDescription();
            if (ConfigItem.MinValue != int.MinValue)
            {
                description += $"\nMin: {ConfigItem.MinValue}";
            }
            if (ConfigItem.MaxValue != int.MaxValue)
            {
                description += $"\nMax: {ConfigItem.MaxValue}";
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
            if (int.TryParse(value, out var newValue))
            {
                ConfigItem.CurrentValue = Math.Clamp(newValue, ConfigItem.MinValue, ConfigItem.MaxValue);
            }
            UpdateAppearance();
            audioManager.PlayChangeValueSFX();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            textInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue}");
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;
        }
    } 
}
