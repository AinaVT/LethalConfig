using LethalConfig.ConfigItems.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LethalConfig.ConfigItems.Options
{
    public sealed class TextInputFieldOptions: BaseOptions
    {
        public int CharacterLimit { get; set; } = 0;
    }
}
