using System.Collections;
using LethalConfig.ConfigItems;
using LethalConfig.MonoBehaviours.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.Components
{
    internal class TextInputFieldController : ModConfigController<TextInputFieldConfigItem, string>
    {
        public TMP_InputField textInputField;

        private LayoutElement _layoutElement;

        protected override void Awake()
        {
            base.Awake();

            _layoutElement = GetComponent<LayoutElement>();
        }

        private void Update()
        {
            if (!textInputField.isFocused || textInputField.lineType == TMP_InputField.LineType.SingleLine) return;

            var isShiftPressed = Keyboard.current.shiftKey.isPressed;
            textInputField.lineType = isShiftPressed
                ? TMP_InputField.LineType.MultiLineNewline
                : TMP_InputField.LineType.MultiLineSubmit;
        }

        protected override void OnSetConfigItem()
        {
            textInputField.text = ConfigItem.CurrentValue;
            var lines = Mathf.Clamp(ConfigItem.NumberOfLines <= 0 ? 4 : ConfigItem.NumberOfLines, 1, 4);
            var height = (float)(16 + lines * 19);
            _layoutElement.minHeight = height;
            _layoutElement.preferredHeight = height;
            textInputField.lineType =
                lines == 1 ? TMP_InputField.LineType.SingleLine : TMP_InputField.LineType.MultiLineSubmit;
            textInputField.lineLimit = ConfigItem.NumberOfLines;
            UpdateAppearance();
        }

        public void OnInputFieldEndEdit(string value)
        {
            var caretPosition = textInputField.caretPosition;
            if (ConfigItem.NumberOfLines != 1) StartCoroutine(RemoveNewLineFromSubmitDelayed(caretPosition));

            ConfigItem.CurrentValue = ConfigItem.TrimText ? value.Trim() : value;
            UpdateAppearance();
            ConfigMenuManager.Instance.menuAudio.PlayChangeValueSfx();
        }

        public override void UpdateAppearance()
        {
            base.UpdateAppearance();
            textInputField.text = ConfigItem.CurrentValue;
            textInputField.textComponent.rectTransform.localPosition = Vector3.zero;
        }

        // This is a workaround because the TMP_InputField adds a line break when pressing enter
        // even with the lineType set to MultiLineSubmit. However, it only adds this line AFTER LateUpdate
        // but before rendering. So this is the only way i could find to remove this additional linebreak it adds to the field.

        private IEnumerator RemoveNewLineFromSubmitDelayed(int caretPosition)
        {
            yield return null;

            string text = textInputField.text;
            int textLength = text.Length;

            if (textLength == 0 || !text.EndsWith("\n"))
            {
                yield break;
            }

            int startIndex = Mathf.Clamp(caretPosition, 0, textLength - 1);

            textInputField.text = text.Remove(startIndex, count: 1);
        }
    }
}