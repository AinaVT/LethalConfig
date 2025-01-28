using LethalConfig.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.ColorPicker
{
    internal class SVImageControl : MonoBehaviour, IDragHandler, IPointerClickHandler
    {
        [SerializeField]
        private ColorPickerControl _colorPickerControl;

        [SerializeField]
        private Image _pickerImage;

        private RectTransform _rectTransform, _pickerTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _pickerTransform = _pickerImage.GetComponent<RectTransform>();
            _pickerTransform.localPosition = new Vector2(-(_rectTransform.sizeDelta.x * 0.5f), -(_rectTransform.sizeDelta.y * 0.5f));
        }

        private void UpdateColor(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPosition);

            Vector2 clampedPosition = new Vector2(
                Mathf.Clamp(localPosition.x, -_rectTransform.rect.width * 0.5f, _rectTransform.rect.width * 0.5f),
                Mathf.Clamp(localPosition.y, -_rectTransform.rect.height * 0.5f, _rectTransform.rect.height * 0.5f)
            );

            float normalizedSaturation = (clampedPosition.x + (_rectTransform.rect.width * 0.5f)) / _rectTransform.rect.width;
            float normalizedValue = (clampedPosition.y + (_rectTransform.rect.height * 0.5f)) / _rectTransform.rect.height;

            _pickerTransform.localPosition = clampedPosition;

            _pickerImage.color = Color.HSVToRGB(0, 0, 1 - normalizedValue);

            _colorPickerControl.SetSatVal(normalizedSaturation, normalizedValue);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            UpdateColor(eventData);
        }

        public void SetPickerLocation(float saturation, float value)
        {
            float x = (saturation * _rectTransform.rect.width) - (_rectTransform.rect.width * 0.5f);
            float y = (value * _rectTransform.rect.height) - (_rectTransform.rect.height * 0.5f);

            _pickerTransform.localPosition = new Vector2(x, y);
            _pickerImage.color = Color.HSVToRGB(0, 0, 1 - value);

            //Debug.Log($"SetPickerLocation (saturation: {saturation}, value: {value}), (x: {x}, y: {y})");
        }
    }
}
