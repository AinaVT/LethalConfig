using System;
using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class FloatSliderController : ModConfigController<FloatSliderConfigItem, float>
    {
        public Slider sliderComponent;
        public TMP_InputField valueInputField;

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\nMin: {ConfigItem.MinValue:0.0#}\nMax: {ConfigItem.MaxValue:0.0#}";
        }

        protected override void OnSetConfigItem()
        {
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);
            sliderComponent.maxValue = ConfigItem.MaxValue;
            sliderComponent.minValue = ConfigItem.MinValue;

            UpdateAppearance();
        }

        public void OnSliderValueChanged(float value)
        {
            ConfigItem.CurrentValue = value;
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public void OnInputFieldEndEdit(string value)
        {
            if (float.TryParse(value, out var newValue))
                ConfigItem.CurrentValue = Math.Clamp(newValue, ConfigItem.MinValue, ConfigItem.MaxValue);
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);
            valueInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue:0.0#}");
        }
    }
}