namespace LethalConfig.ConfigItems.Options
{
    public sealed class TextDropDownOptions : BaseOptions
    {
        public string[] Values;

        public TextDropDownOptions(params string[] values)
        {
            Values = values;
        }

        internal bool HasValues => Values is { Length: > 0 };
    }
}