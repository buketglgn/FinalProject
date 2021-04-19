using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        //Adapter Pattern
        //var olan bir sistemi kendi sistemimize uyarlıyoruz.

        IMemoryCache _memoryCache;  //bir interfacetir.
        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value,TimeSpan.FromMinutes(duration));//timespan=> duration verilen süre karad cache te kalır
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key); //tip belirtmiyoruz ama
            //tip dönüsümü yapılması gerekir. bu yöntem ekstra olarak yazıldı
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); //bellekte olup olmadıgını kontrol ediyoruz.
            //datayı istemiyoruz sadece bool degeri istiyoruz. out _ bir sey yapmaz.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            //ona verdiğimiz bir pattern e göre silme işlemi yapacak.

            //bellege bak, enriesCollection adında bir seye atılıyormus(.net dökümanlarına bakılıp söyleniyor)
            //definition u memeorycache olanları bul ve her bir cache elemanını gez ve bunlardan o cache datasından
            //bizim verdigimize uyanlardan varsa (uyanların keylerini bulup) bellekten uçuruyoruz.
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic;
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            foreach (var cacheItem in cacheEntriesCollection)
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }

            //patern bu sekilde olusturuluyor.
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
