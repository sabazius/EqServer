using EqServer.EqModels.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading;
using MongoDB.Driver;

namespace EqServer.DL.Caches
{
    public class Cache<TKey, TItem>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public TItem GetOrAdd(TKey key, Func<TItem> create)
        {
            TItem cacheItem;

            if (!_cache.TryGetValue(key, out cacheItem))
            {
                cacheItem = create();

                _cache.Set(key, cacheItem);
            }

            return cacheItem;
        }

        private void Consume(CancellationToken token)
        {
            
        }

        public void Init(Dictionary<TKey, TItem> initCache)
        {
            foreach (var item in initCache)
            {
                _cache.Set(item.Key, item.Value);
            }
        }
    }
}
