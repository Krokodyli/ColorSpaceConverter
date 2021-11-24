using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ColorProfileConverter.Models;

namespace ColorProfileConverter.Data
{
    [Serializable]
    public class PredefinedColorProfiles
    {
        [XmlArray(ElementName="Profiles")]
        [XmlArrayItem(ElementName="Profile")]
        public List<PredefinedColorProfile> PredefinedProfiles { get; set; }

        public PredefinedColorProfiles()
        {
            PredefinedProfiles = new List<PredefinedColorProfile>();
        }

        public Dictionary<String, ColorProfile> GetPredefinedProfiles()
        {
            var dictionary = new Dictionary<String, ColorProfile>();
            foreach (var profile in PredefinedProfiles)
                dictionary[profile.Key] = profile.Config;
            return dictionary;
        }
    }

    [Serializable]
    public class PredefinedColorProfile
    {
        public string Key { get; set; }
        public ColorProfile Config { get; set; }
    }
}
