using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours.ColorPicker
{
    internal class ColorPickerControl : MonoBehaviour
    {
        [SerializeField]
        private float _currentHue, _currentSat, _currentVal;

        [SerializeField]
        private RawImage _hueImage, _satValImage, _previousOutputImage, _currentOutputImage;

        [SerializeField]
        private Slider _hueSlider;

        [SerializeField]
        private TMP_InputField _hexColorInputField;

        [SerializeField]
        private SVImageControl _svImageControl;

        private Texture2D _hueTexture, _satValTexture, _previousOutputTexture, _currentOutputTexture;

        private bool _updatedHexColorInputFieldInternally;
        private bool _initialized;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            CreateHueImage();
            CreateSatValImage();
            CreatePreviousOutputImage();
            CreateCurrentOutputImage();

            UpdateCurrentOutputImage();
        }

        private void CreateHueImage()
        {
            _hueTexture = new Texture2D(1, 16, TextureFormat.RGBA32, false);
            _hueTexture.wrapMode = TextureWrapMode.Clamp;
            _hueTexture.name = "HueTexture";

            for (int i = 0; i < _hueTexture.height; i++)
            {
                _hueTexture.SetPixel(0, i, Color.HSVToRGB(i / (float)_hueTexture.height, 1f, 1f));
            }

            _hueTexture.Apply();
            _currentHue = 0f;

            _hueImage.texture = _hueTexture;
        }

        private void CreateSatValImage()
        {
            _satValTexture = new Texture2D(16, 16, TextureFormat.RGBA32, false);
            _satValTexture.wrapMode = TextureWrapMode.Clamp;
            _satValTexture.name = "SatValTexture";

            for (int y = 0; y < _satValTexture.height; y++)
            {
                for (int x = 0; x < _satValTexture.width; x++)
                {
                    _satValTexture.SetPixel(x, y, Color.HSVToRGB(_currentHue, x / (float)_satValTexture.width, y / (float)_satValTexture.height));
                }
            }

            _satValTexture.Apply();
            _currentSat = 0f;
            _currentVal = 0f;

            _satValImage.texture = _satValTexture;
        }

        private void CreatePreviousOutputImage()
        {
            _previousOutputTexture = new Texture2D(1, 16, TextureFormat.RGBA32, false);
            _previousOutputTexture.wrapMode = TextureWrapMode.Clamp;
            _previousOutputTexture.name = "PreviousOutputTexture";

            Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

            for (int i = 0; i < _previousOutputTexture.height; i++)
            {
                _previousOutputTexture.SetPixel(0, i, currentColor);
            }

            _previousOutputTexture.Apply();
            _previousOutputImage.texture = _previousOutputTexture;
        }

        private void CreateCurrentOutputImage()
        {
            _currentOutputTexture = new Texture2D(1, 16, TextureFormat.RGBA32, false);
            _currentOutputTexture.wrapMode = TextureWrapMode.Clamp;
            _currentOutputTexture.name = "CurrentOutputTexture";

            Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

            for (int i = 0; i < _currentOutputTexture.height; i++)
            {
                _currentOutputTexture.SetPixel(0, i, currentColor);
            }

            _currentOutputTexture.Apply();
            _currentOutputImage.texture = _currentOutputTexture;
        }

        private void SetPreviousOutputImage(string hexColor)
        {
            if (!ColorUtility.TryParseHtmlString(hexColor, out Color newColor))
            {
                return;
            }

            for (int i = 0; i < _previousOutputTexture.height; i++)
            {
                _previousOutputTexture.SetPixel(0, i, newColor);
            }

            _previousOutputTexture.Apply();
        }

        private void UpdateCurrentOutputImage()
        {
            Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);

            for (int i = 0; i < _currentOutputTexture.height; i++)
            {
                _currentOutputTexture.SetPixel(0, i, currentColor);
            }

            _currentOutputTexture.Apply();

            _updatedHexColorInputFieldInternally = true;
            _hexColorInputField.text = GetHexColor();
        }

        public void SetSatVal(float saturation, float value)
        {
            _currentSat = saturation;
            _currentVal = value;

            UpdateCurrentOutputImage();
        }

        public void UpdateSatValImage()
        {
            _currentHue = _hueSlider.value;

            for (int y = 0; y < _satValTexture.height; y++)
            {
                for (int x = 0; x < _satValTexture.width; x++)
                {
                    _satValTexture.SetPixel(x, y, Color.HSVToRGB(_currentHue, x / (float)_satValTexture.width, y / (float)_satValTexture.height));
                }
            }

            _satValTexture.Apply();

            UpdateCurrentOutputImage();
        }

        public void OnHexColorInputFieldValueChanged()
        {
            if (_updatedHexColorInputFieldInternally)
            {
                _updatedHexColorInputFieldInternally = false;
                return;
            }

            if (_hexColorInputField.text.Length < 6) return;

            string hexColor;

            if (_hexColorInputField.text.StartsWith("#"))
            {
                hexColor = _hexColorInputField.text;
            }
            else
            {
                hexColor = $"#{_hexColorInputField.text}";
            }

            UpdateColor(hexColor);
        }

        private void UpdateColor(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out Color newColor))
            {
                Color.RGBToHSV(newColor, out _currentHue, out _currentSat, out _currentVal);

                _hueSlider.value = _currentHue;

                UpdateCurrentOutputImage();

                _svImageControl.SetPickerLocation(_currentSat, _currentVal);
            }
        }

        public void SetColor(string hexColor)
        {
            Initialize();

            _updatedHexColorInputFieldInternally = true;
            _hexColorInputField.text = hexColor;

            SetPreviousOutputImage(hexColor);

            UpdateColor(hexColor);
        }

        public string GetHexColor()
        {
            Color currentColor = Color.HSVToRGB(_currentHue, _currentSat, _currentVal);
            return $"#{ColorUtility.ToHtmlStringRGB(currentColor)}";
        }
    }
}
