using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Cache
{
    public interface ICache
    {
       object  Get(string key);
        T Get<T>(string key);

        bool TryGetValue(string key, out object value);
        bool TryGetValue<T>(string key, out T value);
        object Set(string key,object value);
        T Set<T>(string key, T value);
    }
}
