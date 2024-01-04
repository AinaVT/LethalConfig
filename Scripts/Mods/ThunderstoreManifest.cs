using System.Collections.Generic;
using Newtonsoft.Json;

namespace LethalConfig.Mods
{
    internal class ThunderstoreManifest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("version_number")]
        public string VersionNumber { get; set; }
        
        [JsonProperty("website_url")]
        public string WebsiteURL { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("dependencies")]
        public IList<string> Dependencies { get; set; }
    }
}