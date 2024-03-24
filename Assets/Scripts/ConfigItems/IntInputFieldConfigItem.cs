using BepInEx.Configuration;
using LethalConfig.ConfigItems.Options;
using LethalConfig.Utils;
using UnityEngine;

namespace LethalConfig.ConfigItems
{
    public sealed class IntInputFieldConfigItem : BaseValueConfigItem<int>
    {
        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, bool requiresRestart = true) : this(configEntry,
            GetDefaultOptions(configEntry, requiresRestart))
        {
        }

        public IntInputFieldConfigItem(ConfigEntry<int> configEntry, IntInputFieldOptions options) : base(configEntry,
            options)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            MinValue = options.IsMinSet ? options.Min : (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? 0;
            MaxValue = options.IsMaxSet
                ? options.Max
                : (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? 100;
        }

        internal int MaxValue { get; private set; }
        internal int MinValue { get; private set; }

        internal override GameObject CreateGameObjectForConfig()
        {
            return Object.Instantiate(Assets.IntInputFieldPrefab);
        }

        private static IntInputFieldOptions GetDefaultOptions(ConfigEntry<int> configEntry, bool requiresRestart = true)
        {
            var acceptableValues = configEntry.Description.AcceptableValues;

            return new IntInputFieldOptions
            {
                Min = (acceptableValues as AcceptableValueRange<int>)?.MinValue ?? int.MinValue,
                Max = (acceptableValues as AcceptableValueRange<int>)?.MaxValue ?? int.MaxValue,
                RequiresRestart = requiresRestart
            };
        }
    }
}