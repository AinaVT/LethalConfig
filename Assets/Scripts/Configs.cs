using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.MonoBehaviours.Managers;

namespace LethalConfig
{
    internal static class Configs
    {
        enum TestEnum
        {
            None,
            First,
            Second
        }

        internal static ConfigEntry<bool> IsLethalConfigHidden { get; private set; }

        internal static void Initialize(ConfigFile config)
        {
            CreateConfigs(config);
            CreateExampleConfigs(config);
        }

        private static void CreateConfigs(ConfigFile config)
        {
            IsLethalConfigHidden = config.Bind<bool>("General", "Hide Lethal Config", false, "Hides the LethalConfig menu in the game. This setting will not show up in LethalConfig itself.");
            LethalConfigManager.SkipAutoGenFor(IsLethalConfigHidden);
        }

        private static void CreateExampleConfigs(ConfigFile config)
        {
            var intSlider = config.Bind<int>("Example", "Int Slider", 30, new ConfigDescription("This is an integer slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<int>(0, 100)));
            var floatSlider = config.Bind<float>("Example", "Float Slider", 0.0f, new ConfigDescription("This is a float slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var floatStepSlider = config.Bind<float>("Example", "Float Step Slider", 0.0f, new ConfigDescription("This is a float step slider. It set values in increments. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var boolCheckBox = config.Bind<bool>("Example", "Bool Checkbox", false, new ConfigDescription("This is a bool checkbox."));
            var enumDropDown = config.Bind<TestEnum>("Example", "Enum Dropdown", TestEnum.None, new ConfigDescription("This is a enum dropdown."));
            var textInput = config.Bind<string>("Example", "Text Input", "Example", "This is a text input field. It can have a limit of characters too.");
            var multiLineInput = config.Bind<string>("Example", "Multiline Text Input", "Example", "This is a text input field. It can have a limit of characters too.");
            var textInputDropdown = config.Bind<string>("Example", "Text Input Dropdown", "Two", new ConfigDescription("This is a text input with an acceptable value list.", new AcceptableValueList<string>("One", "Two", "HL:Alyx")));
            var intInput = config.Bind<int>("Example", "Int Input", 50, "This is an integer input field.");
            var floatInput = config.Bind<float>("Example", "Float Input", 0.5f, "This is a float input field.");

            LethalConfigManager.AddConfigItem(new IntSliderConfigItem(intSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(floatSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatStepSliderConfigItem(floatStepSlider, new FloatStepSliderOptions() { Step = 0.1f, RequiresRestart = false, Min = -1.0f, Max = 1.0f }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(boolCheckBox, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<TestEnum>(enumDropDown, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(textInput, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(multiLineInput, new TextInputFieldOptions
            {
                NumberOfLines = 0,
                TrimText = true
            }));
            LethalConfigManager.AddConfigItem(new GenericButtonConfigItem("Example", "Button", "This is a test button with a callback", "Open", () =>
            {
                ConfigMenuManager.Instance?.DisplayNotification("Buttons can be used to open custom menus or other things.", "OK");
            }));
            LethalConfigManager.AddConfigItem(new IntInputFieldConfigItem(intInput, new IntInputFieldOptions()
            {
                Max = 150
            }));
            LethalConfigManager.AddConfigItem(new FloatInputFieldConfigItem(floatInput, new FloatInputFieldOptions()
            {
                Max = 2.5f
            }));
            LethalConfigManager.AddConfigItem(new TextDropDownConfigItem(textInputDropdown, new TextDropDownOptions() { RequiresRestart = false }));
        }
    }
}
