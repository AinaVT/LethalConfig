using System;
using System.Collections.Generic;
using System.Linq;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class EnumDropDownController : ModConfigController
    {
        public TMP_Dropdown dropdownComponent;
        private List<string> _enumNames = new();

        private Type _enumType;

        private void OnDestroy()
        {
            BaseConfigItem.OnCurrentValueChanged -= OnCurrentValueChanged;
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\n\nDefault: {Enum.GetName(_enumType, BaseConfigItem.BoxedDefaultValue)}";
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            var index = _enumNames.FindIndex(e => e == Enum.GetName(_enumType, BaseConfigItem.CurrentBoxedValue));
            dropdownComponent.SetValueWithoutNotify(index);
        }

        protected override void OnSetConfigItem()
        {
            BaseConfigItem.OnCurrentValueChanged += OnCurrentValueChanged;

            _enumType = BaseConfigItem.BaseConfigEntry.SettingType;
            _enumNames = Enum.GetNames(_enumType).ToList();

            dropdownComponent.ClearOptions();
            dropdownComponent.AddOptions(_enumNames);
            var index = _enumNames.FindIndex(e => e == Enum.GetName(_enumType, BaseConfigItem.CurrentBoxedValue));
            dropdownComponent.SetValueWithoutNotify(index);
            UpdateAppearance();
        }

        public void OnDropDownValueChanged(int index)
        {
            BaseConfigItem.CurrentBoxedValue = Enum.Parse(_enumType, _enumNames[index]);
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        private void OnCurrentValueChanged()
        {
            UpdateAppearance();
        }
    }
}