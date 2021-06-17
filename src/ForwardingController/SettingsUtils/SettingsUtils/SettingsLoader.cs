using Newtonsoft.Json;
using SettingsUtils.SharedItems.ConfigurationEntities;
using SharedItems;
using System.Collections.Generic;
using System.IO;

namespace SettingsUtils
{
    internal class SettingsLoader
    {
        internal IDictionary<string, object> LoadSettings(string configFile)
        {
            var dic = new Dictionary<string, object>();

            if (!File.Exists(configFile))
                throw new FileNotFoundException($"Configuration file {configFile} couldn't be found!");

            dic[Const.AppSettings] = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(configFile));

            return dic;
        }
    }
}