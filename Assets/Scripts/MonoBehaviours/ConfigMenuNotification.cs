using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenuNotification : MonoBehaviour
    {
        public TextMeshProUGUI messageTextComponent;
        public TextMeshProUGUI buttonTextComponent;

        public void SetNotificationContent(string text, string button)
        {
            messageTextComponent.text = text;
            buttonTextComponent.text = button;
        }
    }
}
