using LethalConfig.ConfigItems;
using LethalConfig.Utils;
using SharedUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class FloatStepSliderController : ModConfigController<FloatStepSliderConfigItem, float>
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
            var numberOfSteps = (int)MathF.Ceiling((ConfigItem.MaxValue - ConfigItem.MinValue) / MathF.Max(ConfigItem.Step, float.Epsilon));
            sliderComponent.maxValue = numberOfSteps;
            sliderComponent.minValue = 0;
            sliderComponent.wholeNumbers = true;
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);

            UpdateAppearance();
        }

        public void OnSliderValueChanged(float value)
        {
            ConfigItem.CurrentValue = MathF.Round(ConfigItem.MinValue + (ConfigItem.Step * (int)value), 4);
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
            sliderComponent.SetValueWithoutNotify((ConfigItem.CurrentValue - ConfigItem.MinValue) / MathF.Max(ConfigItem.Step, float.Epsilon));
            nameTextComponent.text = $"{(ConfigItem.HasValueChanged ? "*" : "")}{ConfigItem.Name}";
            valueInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue:0.0#}");
        }
    } 
}
