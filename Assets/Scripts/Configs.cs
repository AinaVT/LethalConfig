using BepInEx.Configuration;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.MonoBehaviours.Managers;

namespace LethalConfig
{
    internal static class Configs
    {
        internal static ConfigEntry<bool> IsLethalConfigHidden { get; private set; }
        internal static ConfigEntry<bool> AddSectionButtons { get; private set; }
        internal static ConfigEntry<bool> SectionsDefaultClosed { get; private set; }
        internal static ConfigEntry<bool> HideSearchBars { get; private set; }

        internal static void Initialize(ConfigFile config)
        {
            CreateConfigs(config);
            CreateExampleConfigs(config);
        }

        private static void CreateConfigs(ConfigFile config)
        {
            IsLethalConfigHidden = config.Bind("General", "Hide Lethal Config", false,
                "Hides the LethalConfig menu in the game. This setting will not show up in LethalConfig itself.");
            LethalConfigManager.SkipAutoGenFor(IsLethalConfigHidden);
            AddSectionButtons = config.Bind("General", "AddSectionButtons", true, "Add Section buttons to show/hide config items in a section.");
            LethalConfigManager.SkipAutoGenFor(AddSectionButtons); //toggling this mid-game will not update the prefab
            SectionsDefaultClosed = config.Bind("General", "SectionsDefaultClosed", false,
                "Hides all config items by default when AddSectionButtons is enabled.");
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(SectionsDefaultClosed, false));
            HideSearchBars = config.Bind("General", "HideSearchBars", false,
                "Hides all search bars.");
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(HideSearchBars, false));
        }

        private static void CreateExampleConfigs(ConfigFile config)
        {
            
            var intSlider = config.Bind("Example", "Int Slider", 30,
                new ConfigDescription(
                    "This is an integer slider. You can also type a value in the input field to the right of the slider.",
                    new AcceptableValueRange<int>(0, 100)));
            var floatSlider = config.Bind("Example", "Float Slider", 0.0f,
                new ConfigDescription(
                    "This is a float slider. You can also type a value in the input field to the right of the slider.",
                    new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var floatStepSlider = config.Bind("Example", "Float Step Slider", 0.0f,
                new ConfigDescription(
                    "This is a float step slider. It set values in increments. You can also type a value in the input field to the right of the slider.",
                    new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var boolCheckBox = config.Bind("Example", "Bool Checkbox", false,
                new ConfigDescription("This is a bool checkbox."));
            var enumDropDown = config.Bind("Example", "Enum Dropdown", TestEnum.None,
                new ConfigDescription("This is a enum dropdown."));
            var textInput = config.Bind("Example", "Text Input", "Example",
                "This is a text input field. It can have a limit of characters too.");
            var multiLineInput = config.Bind("Example", "Multiline Text Input", "Example",
                "This is a text input field. It can have a limit of characters too.");
            var textInputDropdown = config.Bind("Example", "Text Input Dropdown", "Two",
                new ConfigDescription("This is a text input with an acceptable value list.",
                    new AcceptableValueList<string>("One", "Two", "HL:Alyx")));
            var hexColorInputField = config.Bind("Example", "Hex Color Input", "#FFFFFF",
                "This is a hex color input field. You can preview the color and it includes a color picker!");
            var intInput = config.Bind("Example", "Int Input", 50, "This is an integer input field.");
            var floatInput = config.Bind("Example", "Float Input", 0.5f, "This is a float input field.");

            LethalConfigManager.AddConfigItem(new IntSliderConfigItem(intSlider, false));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(floatSlider, false));
            LethalConfigManager.AddConfigItem(new FloatStepSliderConfigItem(floatStepSlider,
                new FloatStepSliderOptions { Step = 0.1f, RequiresRestart = false, Min = -1.0f, Max = 1.0f }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(boolCheckBox, false));
            LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<TestEnum>(enumDropDown, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(textInput, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(multiLineInput, new TextInputFieldOptions
            {
                NumberOfLines = 0,
                TrimText = true
            }));
            LethalConfigManager.AddConfigItem(new TextDropDownConfigItem(textInputDropdown,
                new TextDropDownOptions { RequiresRestart = false }));
            LethalConfigManager.AddConfigItem(new HexColorInputFieldConfigItem(hexColorInputField,
                new HexColorInputFieldOptions { RequiresRestart = false }));
            LethalConfigManager.AddConfigItem(new IntInputFieldConfigItem(intInput, new IntInputFieldOptions
            {
                Max = 150
            }));
            LethalConfigManager.AddConfigItem(new FloatInputFieldConfigItem(floatInput, new FloatInputFieldOptions
            {
                Max = 2.5f
            }));
            LethalConfigManager.AddConfigItem(new GenericButtonConfigItem("Example", "Button",
                "This is a test button with a callback", "Open", () =>
                {
                    if (ConfigMenuManager.Instance)
                        ConfigMenuManager.DisplayNotification("This is a test notification", "OK");
                }));
        }

        private enum TestEnum
        {
            None,
            First,
            Second
        }
    }
}