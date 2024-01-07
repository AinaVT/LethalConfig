using LethalConfig.MonoBehaviours.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class EnumDropDownController : ModConfigController
    {
        public TMP_Dropdown dropdownComponent;

        private Type _enumType;
        private List<string> _enumNames = new List<string>();

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\n\nDefault: {Enum.GetName(_enumType, baseConfigItem.BoxedDefaultValue)}";
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            var index = _enumNames.FindIndex(e => e == Enum.GetName(_enumType, baseConfigItem.CurrentBoxedValue));
            dropdownComponent.SetValueWithoutNotify(index);
        }

        protected override void OnSetConfigItem()
        {
            _enumType = baseConfigItem.BaseConfigEntry.SettingType;
            _enumNames = Enum.GetNames(_enumType).ToList();

            dropdownComponent.ClearOptions();
            dropdownComponent.AddOptions(_enumNames);
            var index = _enumNames.FindIndex(e => e == Enum.GetName(_enumType, baseConfigItem.CurrentBoxedValue));
            dropdownComponent.SetValueWithoutNotify(index);
            UpdateAppearance();
        }

        public void OnDropDownValueChanged(int index)
        {
            baseConfigItem.CurrentBoxedValue = Enum.Parse(_enumType, _enumNames[index]);
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSFX();
        }
    }
}
