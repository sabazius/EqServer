using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace EqServer.DL.Caches
{
    public class Cache<TItem>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());


        public TItem GetOrAdd(object key, Func<TItem> create)
        {
            TItem cacheItem;

            if (!_cache.TryGetValue(key, out cacheItem))
            {
                cacheItem = create();

                _cache.Set(key, cacheItem);
            }

            return cacheItem;
        }

        //private Dictionary<object, TItem> _cache = new Dictionary<object, TItem>();


        //public TItem GetOrAdd(object key, Func<TItem> createItem)
        //{
        //    if (!_cache.ContainsKey(key))
        //    {
        //        _cache[key] = createItem();
        //    }

        //    return _cache[key];
        //}

    }
}
