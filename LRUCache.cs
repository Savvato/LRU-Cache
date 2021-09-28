namespace LRUCache
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

    public class LRUCache<T> where T : class
    {
        private readonly long capacity;

        private readonly ConcurrentDictionary<string, ItemHolder> cache;

        public LRUCache(long capacity)
        {
            this.capacity = capacity;
            this.cache = new ConcurrentDictionary<string, ItemHolder>();
        }

        public T Get(string key)
        {
            if (this.cache.TryGetValue(key, out ItemHolder value))
            {
                this.cache.AddOrUpdate(
                    key,
                    addValueFactory: key => new ItemHolder { Item = value.Item, Counter = value.Counter + 1 },
                    updateValueFactory: (key, holder) => { holder.Counter++; return holder; });
                return value.Item;
            }
            return null;
        }

        public void Put(string key, T item)
        {
            this.cache.AddOrUpdate(key,
                addValueFactory: key => this.AddValue(key, item),
                updateValueFactory: (key, holder) => { holder.Item = item; return holder; });
        }

        private ItemHolder AddValue(string key, T item)
        {
            if (this.cache.Count >= capacity)
            {
                KeyValuePair<string, ItemHolder> lru = this.cache.FirstOrDefault();
                foreach (KeyValuePair<string, ItemHolder> pair in this.cache)
                {
                    if (lru.Value.Counter > pair.Value.Counter)
                        lru = pair;
                }
                this.cache.TryRemove(lru.Key, out ItemHolder _);
            }
            return new ItemHolder { Item = item };
        }

        public int Count() => this.cache.Count; // for testing purposes

        public bool ContainsKey(string key) => this.cache.ContainsKey(key); // for testing purposes

        private class ItemHolder
        {
            public T Item { get; set; }
            public long Counter { get; set; }
        }
    }
}