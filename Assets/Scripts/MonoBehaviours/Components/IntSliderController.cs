using System;
using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class IntSliderController : ModConfigController<IntSliderConfigItem, int>
    {
        public Slider sliderComponent;
        public TMP_InputField valueInputField;

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\nMin: {ConfigItem.MinValue}\nMax: {ConfigItem.MaxValue}";
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
            ConfigItem.CurrentValue = (int)value;
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public void OnInputFieldEndEdit(string value)
        {
            if (int.TryParse(value, out var newValue))
                ConfigItem.CurrentValue = Math.Clamp(newValue, ConfigItem.MinValue, ConfigItem.MaxValue);
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            sliderComponent.SetValueWithoutNotify(ConfigItem.CurrentValue);
            valueInputField.SetTextWithoutNotify($"{ConfigItem.CurrentValue}");
        }
    }
}