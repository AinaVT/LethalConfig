using System.Collections.Generic;
using System.Linq;
using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;

// Adapted from EnumDropDownController
namespace LethalConfig.MonoBehaviours.Components
{
    internal class TextDropDownController : ModConfigController<TextDropDownConfigItem, string>
    {
        public TMP_Dropdown dropdownComponent;

        private List<string> _textValues = new();

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            var index = _textValues.FindIndex(e => e == (string)BaseConfigItem.CurrentBoxedValue);
            dropdownComponent.SetValueWithoutNotify(index);
        }

        protected override void OnSetConfigItem()
        {
            _textValues = ConfigItem.Values.ToList();

            dropdownComponent.ClearOptions();
            dropdownComponent.AddOptions(_textValues);
            var index = _textValues.FindIndex(e => e == (string)BaseConfigItem.CurrentBoxedValue);
            dropdownComponent.SetValueWithoutNotify(index);
            UpdateAppearance();
        }

        public void OnDropDownValueChanged(int index)
        {
            ConfigItem.CurrentValue = _textValues[index];
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }
    }
}