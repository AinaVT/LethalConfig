using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using LethalConfig.ConfigItems;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Settings;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LethalConfig
{
    internal static class PluginInfo
    {
        public const string Guid = "ainavt.lc.lethalconfig";
        public const string Name = "LethalConfig";
        public const string Version = "1.0.0";
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

            LogUtils.Init();
            Prefabs.Init();
            SettingsUI.Init();

            CreateExampleConfigs();

            LogUtils.LogInfo("LethalConfig loaded!");
        }

        private void CreateExampleConfigs()
        {
            var intSlider = Config.Bind<int>("Non-Restart Example", "Int Slider", 30, new ConfigDescription("This is a integer slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<int>(0, 100)));
            var floatSlider = Config.Bind<float>("Non-Restart Example", "Float Slider", 0.0f, new ConfigDescription("This is a float slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var floatStepSlider = Config.Bind<float>("Non-Restart Example", "Float Step Slider", 0.0f, new ConfigDescription("This is a float step slider. It set values in increments. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var boolCheckBox = Config.Bind<bool>("Non-Restart Example", "Bool Checkbox", false, new ConfigDescription("This is a bool checkbox."));
            var enumDropDown = Config.Bind<TestEnum>("Non-Restart Example", "Enum Dropdown", TestEnum.None, new ConfigDescription("This is a enum dropdown."));
            var textInput = Config.Bind<string>("Non-Restart Example", "Text Input", "Example", "This is a text input field. It can have a limit of characters too.");


            var intSliderRestart = Config.Bind<int>("Restart Example", "Int Slider", 30, new ConfigDescription("This is a integer slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<int>(0, 100)));
            var floatSliderRestart = Config.Bind<float>("Restart Example", "Float Slider", 0.0f, new ConfigDescription("This is a float slider. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var floatStepSliderRestart = Config.Bind<float>("Restart Example", "Float Step Slider", 0.0f, new ConfigDescription("This is a float step slider. It set values in increments. You can also type a value in the input field to the right of the slider.", new AcceptableValueRange<float>(-1.0f, 1.0f)));
            var boolCheckBoxRestart = Config.Bind<bool>("Restart Example", "Bool Checkbox", false, new ConfigDescription("This is a bool checkbox."));
            var enumDropDownRestart = Config.Bind<TestEnum>("Restart Example", "Enum Dropdown", TestEnum.None, new ConfigDescription("This is a enum dropdown."));
            var textInputRestart = Config.Bind<string>("Restart Example", "Text Input", "Example", "This is a text input field. It can have a limit of characters too.");

            LethalConfigManager.AddConfigItem(new IntSliderConfigItem(intSliderRestart));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(floatSliderRestart));
            LethalConfigManager.AddConfigItem(new FloatStepSliderConfigItem(floatStepSliderRestart, new FloatStepSliderOptions() { Step = 0.1f, Min = -1.0f, Max = 1.0f }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(boolCheckBoxRestart));
            LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<TestEnum>(enumDropDownRestart));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(textInputRestart));

            LethalConfigManager.AddConfigItem(new IntSliderConfigItem(intSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatSliderConfigItem(floatSlider, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new FloatStepSliderConfigItem(floatStepSlider, new FloatStepSliderOptions() { Step = 0.1f, RequiresRestart = false, Min = -1.0f, Max = 1.0f }));
            LethalConfigManager.AddConfigItem(new BoolCheckBoxConfigItem(boolCheckBox, requiresRestart: false));
            LethalConfigManager.AddConfigItem(new EnumDropDownConfigItem<TestEnum>(enumDropDown, false));
            LethalConfigManager.AddConfigItem(new TextInputFieldConfigItem(textInput, false));
        }
    }

}