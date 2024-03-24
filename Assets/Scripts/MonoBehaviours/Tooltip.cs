using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;

        private RectTransform _rectTransform;
        private Camera _uiCamera;

        public void SetText(string text)
        {
            textComponent.text = text;
        }

        public void SetTarget(GameObject newTargetGameObject)
        {
            transform.position = newTargetGameObject.transform.position;
        }
    }
}