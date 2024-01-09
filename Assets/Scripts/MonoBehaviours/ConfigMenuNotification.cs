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
            var animatorState = notificationAnimator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.IsName("NotificationNormal") || animatorState.IsName("NotificationAppear")) return;

            gameObject.SetActive(true);
            notificationAnimator.SetTrigger("Open");
        }

        public void Close(bool animated = true)
        {
            var animatorState = notificationAnimator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.IsName("NotificationClosed") || animatorState.IsName("NotificationDisappear")) return;

            if (!animated)
            {
                gameObject.SetActive(false);
                notificationAnimator.SetTrigger("ForceClose");
                return;
            }

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
