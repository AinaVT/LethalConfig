![LethalConfig Icon](https://i.imgur.com/fKaf1mS.png "LethalConfig icon")

# LethalConfig

LethalConfig is an in-game mod configuration menu that can be used by any mod developer to let players change their BepInEx's ConfigEntry's within the game through a variety of UI controls.

Inspired by Rune580's [RiskOfOptions](https://github.com/Rune580/RiskOfOptions)

## Summary
- [Supported Types](#supported-types)
- [Usage](#usage)
    - [Setting up](#setting-up)
    - [Adding a ConfigItem](#adding-a-configitem)
    - [ConfigItem restart requirement](#configitem-restart-requirement)
    - [Listening to setting changes](#listening-to-setting-changes)
- [Changelog](#changelog)


## Supported Types

Currently, LethalConfig allows developers to add the following types of interfaces for ConfigEntry's:

- Integer Sliders `int`
- Float Sliders `float`
- Float Step Slider `float`
- Text Input Field `string`
- Enum Dropdown `Enum`
- Boolean Checkbox `bool`

![LethalConfig Menu Example](https://i.imgur.com/nJkGNnj.gif "An example of the LethalConfig menu")
*An example of the LethalConfig menu and its element types*

## Usage

### Setting up

To start using LethalConfig on your mod, add a reference on your project to the LethalConfig's dll file. You can get the dll by downloading the LethalConfig mod on [Thunderstore](https://thunderstore.io/c/lethal-company/p/AinaVT/LethalConfig/).

To access the API, simply use the `LethalConfig` namespace or import it on your source file:
```csharp
using LethalConfig;
```

It is also recommend to add a `BepInDependency` attribute to your plugin to hint BepInEx that your mod has a dependency to it:

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
![Slider example](https://i.imgur.com/c212ZMV.png)

LethalConfig automatically picks up some of your mod info, and it automatically creates sections based on the section of the provided ConfigEntry, so you do not have to worry about any extra setup in terms on layout.

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

## Changelog
### Version 1.0.0
    Initial release, which includes the following types:
        - IntSliderConfigItem
        - FloatSliderConfigItem
        - FloatStepSliderConfigItem
        - EnumDropDownConfigItem
        - BoolCheckBoxConfigItem
        - TextInputFieldConfigItem