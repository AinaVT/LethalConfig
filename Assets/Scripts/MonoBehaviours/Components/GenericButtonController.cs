using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class GenericButtonController : ModConfigController
    {
        public TextMeshProUGUI buttonTextComponent;
        private GenericButtonConfigItem ConfigItem => BaseConfigItem as GenericButtonConfigItem;

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
            ConfigMenuManager.Instance.menuAudio.PlayConfirmSfx();
            ConfigItem?.ButtonOptions?.ButtonHandler?.Invoke();
        }
    }
}