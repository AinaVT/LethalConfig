## Version 1.2.0
- LethalConfig now reads both the mod's icons and descriptions directly from its thunderstore manifest if one can be found. (@Rune580)
- Added methods to make LethalConfig skip the autogeneration for your entire mod, a config section, or individual configs. (@Rune580)
- Added callbacks to the ConfigItem's options to tell whether a field is modifiable or not. (@Rune580)
- Added LethalConfig to the in-game quick menu.

## Version 1.1.0
- Automatically generating mod entries and config items for all loaded mods that creates their own ConfigEntry. With this, mods technically don't have the need to necessarily have LethalConfig as a dependency unless they want to use specific components, mark certain settings as non-restart required, or want to set their mod icon and description.
- Adjusted some layout stuff.
- Fixed a bug with the initialization of sliders.
- Added customizable mod icons and mod descriptions.

## Version 1.0.1
- Added two new config types:
  - IntInputConfigItem (an integer text field)
  - FloatInputConfigItem (a float text field)
- Fixed missing default value in the enum dropdown's description.

## Version 1.0.0
- Initial release, which includes the following types:
  - IntSliderConfigItem
  - FloatSliderConfigItem
  - FloatStepSliderConfigItem
  - EnumDropDownConfigItem
  - BoolCheckBoxConfigItem
  - TextInputFieldConfigItem