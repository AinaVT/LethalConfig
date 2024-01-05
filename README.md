![LethalConfig Icon](.github/images/icon.png "LethalConfig icon")

# LethalConfig <!-- omit in toc -->

LethalConfig is a mod configuration menu that allows players to edit their configs from within the game. It also provides a simple API for developers to customize their mod and config entries.

Inspired by Rune580's [RiskOfOptions](https://github.com/Rune580/RiskOfOptions)

### Summary <!-- omit in toc -->
- [Supported Types](#supported-types)
- [Usage](#usage)
  - [Automatic generation](#automatic-generation)
  - [Setting up](#setting-up)
  - [Adding a ConfigItem](#adding-a-configitem)
  - [ConfigItem restart requirement](#configitem-restart-requirement)
  - [Listening to setting changes](#listening-to-setting-changes)
  - [Customizing mod's icon and description](#customizing-mods-icon-and-description)
- [Building LethalConfig](#building-lethalconfig)
- [Issues and Suggestions](#issues-and-suggestions)
- [Changelog](#changelog)
  - [Version 1.1.0](#version-110)
  - [Version 1.0.1](#version-101)
  - [Version 1.0.0](#version-100)


## Supported Types

Currently, LethalConfig allows developers to add the following types of interfaces for ConfigEntry's:

| Component | Value Type | ConfigItem Type |
| --------- | ---------- | ---------- |
| Integer Slider | `int` | `IntSliderConfigItem` |
| Integer Input Field | `int` | `IntInputFieldConfigItem` |
| Float Slider | `float` | `FloatSliderConfigItem` |
| Float Step Slider | `float` | `FloatStepSliderConfigItem` |
| Float Input Field | `float` | `FloatInputFieldConfigItem` |
| Text Input Field | `string` | `TextInputFieldConfigItem` |
| Enum Dropdown | `Enum` | `EnumDropDownConfigItem<>` |
| Boolean Checkbox | `Enum` | `BoolCheckBoxConfigItem` |


![LethalConfig Menu Example](.github/images/menu-example.gif "An example of the LethalConfig menu")
\
*An example of the LethalConfig menu and its element types*

## Usage

### Automatic generation

As of 1.1.0, LethalConfig automatically generates mod entries and detect all ConfigEntry's declared by said mods, and tries its best to generate the correct UI components.

It'll assign a default mod icon and will have a disclaimer in the mod's and config's descriptions about them being automatically generated and that they may require a restart, as there's no way for LethalConfig to tell if a setting will take effect immediately or not.

Unless you're a mod developer that wants to customize their mod icon and description, manually setup the UI components (e.g. use a slider instead of a number text field), or mark settings as non-restart required, you don't need to do anything else other than installing the mod.

Some types may not have a UI component developed for it yet. In these cases, LethalConfig will ignore the ConfigEntry. More types will be covered over time.

### Setting up

To start using LethalConfig on your mod, add a reference on your project to the LethalConfig's dll file. You can get the dll by downloading the LethalConfig mod on [Thunderstore](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/).

To access the API, simply use the `LethalConfig` namespace or import it on your source file:
```csharp
using LethalConfig;
```

It is also recommended to add a `BepInDependency` attribute to your plugin to hint BepInEx that your mod has a dependency to it:

```csharp
[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
[BepInDependency("ainavt.lc.lethalconfig")]
public class MyVeryCoolPlugin: BaseUnityPlugin {
    ...
}
```

### Adding a ConfigItem

With everything setup, you should now have access to the main `LethalConfigManager` and are able to add any of the available components you want.

First, you create your `ConfigEntry` from BepInEx like you would normally:

```csharp
var configEntry = Config.Bind("General", "Example", 0, "This is an example component!");
```

With your `ConfigEntry` in hand, you can now create and register a new item to the menu by using the following method:

```csharp
var exampleSlider = new IntSliderConfigItem(configEntry, new IntSliderOptions 
{
    Min = 0,
    Max = 100
});
LethalConfigManager.AddConfigItem(exampleSlider);
```

And that's it, you now have created your first component!
![Slider example](.github/images/slider-example.png)

LethalConfig automatically picks up some of your mod info, and it automatically creates sections based on the section of the provided ConfigEntry, so you do not have to worry about any extra setup in terms of layout.

### ConfigItem restart requirement

By default, all items will be set to require a restart if changed. This will give a warning to the player once they apply the settings.

If you want your items to not be flagged as restart required, simply flag the constructor of the config items, either through the `bool` constructor overload or passing it inside the item's specific options:

```csharp
// Using the slider options object
var exampleSlider = new IntSliderConfigItem(configEntry, new IntSliderOptions 
{
    RequiresRestart = false,
    Min = 0,
    Max = 100
});

// Using the bool constructor overload
var exampleSlider = new IntSliderConfigItem(configEntry, requiresRestart: false);
```

### Listening to setting changes

Note that players will most likely expect settings to take effect immediately if not prompted to restart. If you need to listen to changes to values of your configuration, `ConfigEntry` provides a mechanism for that:

```csharp
configEntry.SettingChanged += (obj, args) =>
{
    logSource.LogInfo($"Slider value changed to {configEntry.Value}");
};
```

### Customizing mod's icon and description

LethalConfig automatically attempts to load your mod's icon and description from your Thunderstore manifest, but also allows you to override them.

To override your mod's icon in its entry, simply call `LethalConfigManager.SetModIcon` passing an instance of `UnityEngine.Sprite`. A sprite can be loaded from an AssetBundle.

```csharp
var aVeryCoolIconAsset = assetBundle.LoadAsset<Sprite>("path/to/asset.png")
LethalConfigManager.SetModIcon(aVeryCoolIconAsset);
```

To override your mod's description, simply call `LethalConfigManager.SetModDescription` passing a string.

```csharp
LethalConfigManager.SetModDescription("Very cool mod description!");
```

## Building LethalConfig

If you want to contribute and/or need to build LethalConfig yourself, just open the project and import Lethal Company using the included ThunderKit. To do so, follow these steps to setup your project:

- Clone the repository and open the project with Unity `2022.3.9f1`. 
  - If Unity asks if you want to open in Safe Mode at any point, click to ignore.
- On the ThunderKit window that opened, go to `ThunderKit Settings`, set your game path and executable to your Lethal Company folder and exe file.
  - If the ThunderKit window didn't automatically open, you can find it on `Tools > ThunderKit > Settings`.
- ThunderKit will prompt you to restart the project a couple of times. After the restarts, the project should be setup!

With the project setup, simply select the `Publish` pipeline and the `LethalConfigManifest` at the top of the project, then click `execute`. After it's done, it should generate a zip file under the `ThunderKit/Staging` folder containing the mod.

![Pipeline and Manifest](.github/images/pipeline-and-manifest.png)

![Pipeline and Manifest](.github/images/publish-success.png)

## Issues and Suggestions

If you have an issue with LethalConfig, or want to suggest a feature, feel free to open an issue at the [GitHub repository](https://github.com/AinaVT/LethalConfig).

Alternatively, you can also leave your suggestions and issues on the LethalConfig post under the [Mod Releases forum](https://discord.com/channels/1169792572382773318/1191540198018912336) in the [Unofficial Lethal Company Discord Server](https://discord.gg/nYcQFEpXfU).

## Changelog
### Version 1.1.0
    - Automatically generating mod entries and config items for all loaded mods that creates their own ConfigEntry. With this, mods technically don't have the need to necessarily have LethalConfig as a dependency unless they want to use specific components, mark certain settings as non-restart required, or want to set their mod icon and description.
    - Adjusted some layout stuff.
    - Fixed a bug with the initialization of sliders.
    - Added customizable mod icons and mod descriptions.

### Version 1.0.1
    - Added two new config types:
        - IntInputConfigItem (an integer text field)
        - FloatInputConfigItem (a float text field)
    - Fixed missing default value in the enum dropdown's description.

### Version 1.0.0
    Initial release, which includes the following types:
        - IntSliderConfigItem
        - FloatSliderConfigItem
        - FloatStepSliderConfigItem
        - EnumDropDownConfigItem
        - BoolCheckBoxConfigItem
        - TextInputFieldConfigItem