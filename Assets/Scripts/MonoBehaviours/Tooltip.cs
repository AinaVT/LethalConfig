using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private void Awake()
        {
            rectTransform = transform as RectTransform;
            uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            var mousePos = Mouse.current.position.ReadValue();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent as RectTransform, mousePos, uiCamera, out var convertedPoint))
            {
                rectTransform.localPosition = convertedPoint;
            }
        }

        
    }
}
