using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenuNotification : MonoBehaviour
    {
        public TextMeshProUGUI messageTextComponent;
        public TextMeshProUGUI buttonTextComponent;

        public Animator notificationAnimator;

        public void SetNotificationContent(string text, string button)
        {
            messageTextComponent.text = text;
            buttonTextComponent.text = $"[{button}]";
        }

        public void Open()
        {
            gameObject.SetActive(true);
            notificationAnimator.SetTrigger("Open");
        }

        public void Close()
        {
            notificationAnimator.SetTrigger("Close");
        }

        public void OnButtonClick()
        {
            Close();
            ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX();
        }

        public void OnCloseAnimationEnd()
        {
            gameObject.SetActive(false);
        }
    }
}
