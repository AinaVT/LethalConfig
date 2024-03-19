
using System.Collections.Generic;

namespace LethalConfig.ConfigItems.Options
{
    public sealed class TextDropDownOptions : BaseOptions
    {
        internal bool HasValues => Values != null && Values.Length > 0;
        public string[] Values;

        public TextDropDownOptions(params string[] values) {
            this.Values = values;
        }
    }
}
