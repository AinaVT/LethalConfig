using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class GenericButtonController : ModConfigController
    {
        private GenericButtonConfigItem ConfigItem => baseConfigItem as GenericButtonConfigItem;

        public TextMeshProUGUI buttonTextComponent;

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            buttonTextComponent.text = ConfigItem?.ButtonOptions?.ButtonText ?? "Button";
        }

        protected override void OnSetConfigItem()
        {
            UpdateAppearance();
        }

        public void OnButtonClicked()
        {
            ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX();
            ConfigItem?.ButtonOptions?.ButtonHandler?.Invoke();
        }
    }
}
