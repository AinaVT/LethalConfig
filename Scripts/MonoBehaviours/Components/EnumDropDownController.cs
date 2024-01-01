using LethalConfig.MonoBehaviours.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class EnumDropDownController : ModConfigController
    {
        public TMP_Dropdown dropdownComponent;
        public TextMeshProUGUI nameTextComponent;

        private Type _enumType;
        private List<string> _enumNames = new List<string>();

        public override void UpdateAppearance()
        {
            nameTextComponent.text = $"{(baseConfigItem.HasValueChanged ? "*" : "")}{baseConfigItem.Name}";
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
            audioManager.PlayChangeValueSFX();
        }
    }
}
