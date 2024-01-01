using LethalConfig.ConfigItems;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class FloatSliderController : ModConfigController<FloatSliderConfigItem, float>
    {
        public Slider sliderComponent;
        public TextMeshProUGUI nameTextComponent;
        public TMP_InputField valueInputField;

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\nMin: {ConfigItem.MinValue:0.0#}\nMax: {ConfigItem.MaxValue:0.0#}";
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
            ConfigItem.CurrentValue = value;
            UpdateAppearance();
            audioManager.PlayChangeValueSFX();
        }

        public void OnInputFieldEndEdit(string value)
        {
            if (float.TryParse(value, out var newValue))
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
            valueInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue:0.0#}");
        }
    } 
}
