using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI textComponent;

        private RectTransform rectTransform;
        private Camera uiCamera;

        public void SetText(string text)
        {
            textComponent.text = text;
        }

        public void SetTarget(GameObject gameObject)
        {
            transform.position = gameObject.transform.position;
        }

        
    }
}
