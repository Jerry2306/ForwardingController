using System;

namespace SettingsUtils.Contract
{
    public interface ISettingsManager
    {
        void Add(string key, object value);
        T Get<T>(string key = "");
        void Remove(string key);
    }
}