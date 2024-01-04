using LethalConfig.ConfigItems;
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

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            toggleComponent.SetIsOnWithoutNotify(ConfigItem.CurrentValue);
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
