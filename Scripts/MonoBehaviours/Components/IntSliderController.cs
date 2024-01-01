using LethalConfig.ConfigItems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class IntSliderController : ModConfigController<IntSliderConfigItem, int>
    {
        public Slider sliderComponent;
        public TextMeshProUGUI nameTextComponent;
        public TMP_InputField valueInputField;

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\nMin: {ConfigItem.MinValue}\nMax: {ConfigItem.MaxValue}";
        }

        protected override void OnSetConfigItem()
        {
            sliderComponent.maxValue = ConfigItem.MaxValue;
            sliderComponent.minValue = ConfigItem.MinValue;
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);

            UpdateAppearance();
        }

        public void OnSliderValueChanged(float value)
        {
            ConfigItem.CurrentValue = (int)value;
            UpdateAppearance();
            audioManager.PlayChangeValueSFX();
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
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);
            nameTextComponent.text = $"{(ConfigItem.HasValueChanged ? "*" : "")}{ConfigItem.Name}";
            valueInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue}");
        }
    } 
}
