using LethalConfig.ConfigItems;

namespace LethalConfig.AutoConfig
{
    internal readonly struct ConfigEntryPath
    {
        private readonly string _section;
        private readonly PathSettings _sectionSettings;
        private readonly string _key;
        private readonly PathSettings _keySettings;

        public ConfigEntryPath(string section, string key)
        {
            _section = section;
            _sectionSettings = PathSettings.Exact;

            if (section == "*")
                _sectionSettings = PathSettings.Wildcard;
            
            _key = key;
            _keySettings = PathSettings.Exact;
            
            if (key == "*")
                _keySettings = PathSettings.Wildcard;
        }

        public bool Matches(BaseConfigItem configItem)
        {
            if (_sectionSettings == PathSettings.Exact)
            {
                if (!string.Equals(configItem.Section, _section))
                    return false;
            }

            if (_keySettings == PathSettings.Exact)
            {
                if (!string.Equals(configItem.Name, _key))
                    return false;
            }

            return true;
        }

        private enum PathSettings
        {
            Exact,
            Wildcard
        }
    }
}