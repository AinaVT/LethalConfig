using BepInEx.Configuration;
using UnityEngine;
using LethalConfig.Utils;
using LethalConfig.ConfigItems.Options;

namespace LethalConfig.ConfigItems
{
    public sealed class IntInputFieldConfigItem : BaseValueConfigItem<int>
    {
        internal int MaxValue { get; private set; }
        internal int MinValue { get; private set; }

        public IntInputFieldConfigItem(ConfigEntry<int> configEntry) : this(configEntry, true) { }
        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, bool requiresRestart) : this(configEntry, GetDefaultOptions(configEntry, requiresRestart)) { }
        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, IntInputFieldOptions options) : base(configEntry, options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.WasMinSet ? options.Min : (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0;
            MaxValue = options.WasMaxSet ? options.Max : (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100;
        }

        internal override GameObject CreateGameObjectForConfig()
        {
            return GameObject.Instantiate(Assets.IntInputFieldPrefab);
        }

        private static IntInputFieldOptions GetDefaultOptions(ConfigEntry<int> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new()
            {
                Min = (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? int.MinValue,
                Max = (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? int.MaxValue,
                RequiresRestart = requiresRestart
            };
        }
    } 
}
