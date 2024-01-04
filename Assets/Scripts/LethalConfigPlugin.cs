using BepInEx;
using BepInEx.Configuration;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Settings;
using LethalConfig.Utils;

namespace LethalConfig
{
    internal static class PluginInfo
    {
        public const string Guid = "ainavt.lc.lethalconfig";
        public const string Name = "LethalConfig";
        public const string Version = "1.1.0";
    }

    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    internal class LethalConfigPlugin : BaseUnityPlugin
    {
        private static LethalConfigPlugin instance;

        enum TestEnum
        {
            None,
            First,
            Second
        }

        private void Awake()
        {
            if (instance == null) instance = this;

            LogUtils.Init(PluginInfo.Guid);
            Assets.Init();
            SettingsUI.Init();

            CreateExampleConfigs();

            LethalConfigManager.SetModIcon(Assets.LethalConfigModIcon);
            LethalConfigManager.SetModDescription("Provides an in-game config menu for players to edit their configs, and an API for other mods to use and customize their entries.");

            LogUtils.LogInfo("LethalConfig loaded!");
        }

        private void CreateExampleConfigs()
        {
            var intSlider = Config.Bind<int>("Example", "Int Slider", 30, new ConfigDescription("This is an integer slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<int>(0, 100)));
            var floatSlider = Config.Bind<float>("Example", "Float Slider", 0.0f, new ConfigDescription("This is a float slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var floatStepSlider = Config.Bind<float>("Example", "Float Step Slider", 0.0f, new ConfigDescription("This is a float step slider. It set values in increments. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var boolCheckBox = Config.Bind<bool>("Example", "Bool Checkbox", false, new ConfigDescription("This is a bool checkbox."));
            var enumDropDown = Config.Bind<TestEnum>("Example", "Enum Dropdown", TestEnum.None, new ConfigDescription("This is a enum dropdown."));
            var textInput = Config.Bind<string>("Example", "Text Input", "Example", "This is a text input field. It can have a limit of characters too.");
            var intInput = Config.Bind<int>("Example", "Int Input", 50, "This is an integer input field.");
            var floatInput = Config.Bind<float>("Example", "Float Input", 0.5f, "This is a float input field.");

            LethalConfigManager.AddConfigItem(new IntSliderConfigItem(intSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(floatSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatStepSliderConfigItem(floatStepSlider, new FloatStepSliderOptions() { Step = 0.1f, RequiresRestart = false, Min = -1.0f, Max = 1.0f }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(boolCheckBox, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<TestEnum>(enumDropDown, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(textInput, false));
            LethalConfigManager.AddConfigItem(new IntInputFieldConfigItem(intInput, new IntInputFieldOptions()
            {
                Max = 150
            }));
            LethalConfigManager.AddConfigItem(new FloatInputFieldConfigItem(floatInput, new FloatInputFieldOptions()
            {
                Max = 2.5f
            }));
        }
    }

}