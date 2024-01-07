using LethalConfig.MonoBehaviours.Managers;
using LethalConfig.Utils;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LethalConfig.Settings
{
    internal static class MenusUtils
    {
        internal static void InjectMenu(Transform parentTransform, Transform mainButtonsTransform, GameObject quitButton)
        {
            // Adding manager to scene
            var manager = Object.Instantiate(Assets.ConfigMenuManagerPrefab);
            manager.transform.SetParent(parentTransform); // Same level as menu manager.
            manager.transform.localPosition = Vector3.zero;

            // Adding menu to scene
            var configMenu = Object.Instantiate(Assets.ConfigMenuPrefab);
            configMenu.transform.SetParent(parentTransform, false);
            configMenu.transform.localPosition = Vector3.zero;
            configMenu.transform.localScale = Vector3.one;
            configMenu.transform.localRotation = Quaternion.identity;
            configMenu.SetActive(false);

            // Adding notification ui to scene
            var notification = Object.Instantiate(Assets.ConfigMenuNotificationPrefab);
            notification.transform.SetParent(parentTransform, false);
            notification.transform.localPosition = Vector3.zero;
            notification.transform.localScale = Vector3.one;
            notification.transform.localRotation = Quaternion.identity;
            notification.SetActive(false);

            // Cloning main menu button for opening our menu
            var clonedButton = Object.Instantiate(quitButton, mainButtonsTransform);
            clonedButton.GetComponent<Button>().onClick.RemoveAllListeners();
            clonedButton.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.ShowConfigMenu(); });
            clonedButton.GetComponent<Button>().onClick.AddListener(delegate { ConfigMenuManager.Instance.menuAudio.PlayConfirmSFX(); });

            clonedButton.GetComponentInChildren<TextMeshProUGUI>().text = "> LethalConfig";

            // Offsets all buttons inside the main buttons.
            // This is what makes me wish the game used a vertical layout group :T
            var buttonsList = mainButtonsTransform.GetComponentsInChildren<Button>()
                .Select(b => b.gameObject);

            // Gets the smallest distance between two buttons. Needs this since it turns out different
            // menus have different offsets. Cool.
            // This is awful and would also be unnecessary if the game used a vertial layout group
            // Oh well..
            var positions = buttonsList
                .Where(b => b != clonedButton)
                .Select(b => b.transform as RectTransform)
                .Select(t => t.anchoredPosition.y);
            var offsets = positions
                .Zip(positions.Skip(1), (y1, y2) => Mathf.Abs(y2 - y1));
            var offset = offsets.Min();

            foreach (var button in buttonsList.Where(g => g != quitButton))
            {
                button.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, offset);
            }

            clonedButton.GetComponent<RectTransform>().anchoredPosition = quitButton.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, offset);
        }
    }
}
