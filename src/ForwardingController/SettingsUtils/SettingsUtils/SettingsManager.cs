using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettingsUtils.Contract;

namespace SettingsUtils
{
    public class SettingsManager : ISettingsManager
    {
        private IDictionary<string, object> _settingsRegister;
        public SettingsManager()
        {
            _settingsRegister = new SettingsLoader().LoadSettings(ConfigurationManager.AppSettings["ConfigurationJsonFile"]);
        }

        public void Add(string key, object value)
        {
            if (_settingsRegister.ContainsKey(key))
                throw new ArgumentException("Can't add setting, key already exists!");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Can't add setting, key can't be empty!");

            _settingsRegister.Add(key, value);
        }

        public T Get<T>(string key = "")
        {
            if (!string.IsNullOrEmpty(key))
                return GetViaKey<T>(key);

            var values = _settingsRegister.Where(x => x.Value is T).ToList();

            if (values.Count == 1)
                return (T)values.First().Value;

            throw new Exception($"When getting settings via type they only can occure once: {typeof(T).Name} ({values.Count})");
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key can't be null or empty!");

            _settingsRegister.Remove(key);
        }

        private T GetViaKey<T>(string key)
        {
            if (!_settingsRegister.ContainsKey(key))
                throw new Exception($"Setting with key '{key}' couldn't be found!");
            var value = _settingsRegister[key];

            if (value is T)
                return (T)value;

            throw new Exception($"The value for the key '{key}' is not from type '{typeof(T).Name}'!");
        }
    }
}