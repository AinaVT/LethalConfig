using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class ConfigMenuNotification : MonoBehaviour
    {
        private static readonly int TriggerIdOpen = Animator.StringToHash("Open");
        private static readonly int TriggerIdForceClose = Animator.StringToHash("ForceClose");
        private static readonly int TriggerIdClose = Animator.StringToHash("Close");
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
            notificationAnimator.SetTrigger(TriggerIdOpen);
            transform.SetAsLastSibling();
        }

        public void Close(bool animated = true)
        {
            var animatorState = notificationAnimator.GetCurrentAnimatorStateInfo(0);
            if (animatorState.IsName("NotificationClosed") || animatorState.IsName("NotificationDisappear")) return;

            if (!animated)
            {
                gameObject.SetActive(false);
                notificationAnimator.SetTrigger(TriggerIdForceClose);
                return;
            }

            notificationAnimator.SetTrigger(TriggerIdClose);
        }

        public void OnButtonClick()
        {
            Close();
            ConfigMenuManager.Instance.menuAudio.PlayConfirmSfx();
        }

        public void OnCloseAnimationEnd()
        {
            gameObject.SetActive(false);
        }
    }
}