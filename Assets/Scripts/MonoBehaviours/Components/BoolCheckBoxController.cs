using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
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
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSFX();
        }
    }
}
