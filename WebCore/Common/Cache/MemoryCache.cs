using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Cache
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        public MemoryCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }
        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public  bool TryGetValue(string key,out object value)
        {
            return _memoryCache.TryGetValue(key,out value);
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public object Set(string key, object value)
        {
            return _memoryCache.Set(key, value);
        }

        public T Set<T>(string key, T value)
        {
           return   _memoryCache.Set(key, value);
        }
    }
}
