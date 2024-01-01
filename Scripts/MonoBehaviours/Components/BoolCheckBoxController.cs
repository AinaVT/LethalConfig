using LethalConfig.MonoBehaviours.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class BoolCheckBoxController : ModConfigController<BoolCheckBoxConfigItem, bool>
    {
        public Toggle toggleComponent;
        public TextMeshProUGUI nameTextComponent;

        public override void UpdateAppearance()
        {
            toggleComponent.SetIsOnWithoutNotify(ConfigItem.CurrentValue);
            nameTextComponent.text = $"{(ConfigItem.HasValueChanged ? "*" : "")}{ConfigItem.Name}";
        }

        protected override void OnSetConfigItem()
        {
            toggleComponent.SetIsOnWithoutNotify(ConfigItem.CurrentValue);
            UpdateAppearance();
        }

        public void OnCheckBoxValueChanged(bool value)
        {
            ConfigItem.CurrentValue = value;
            UpdateAppearance();
            audioManager.PlayChangeValueSFX();
        }

        public void OnPointerClicked(BaseEventData eventData)
        {
            var pointerEventData = eventData as PointerEventData;

            if (pointerEventData == null) return;

            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                toggleComponent.isOn = !toggleComponent.isOn;
            }
        }
    }
}
