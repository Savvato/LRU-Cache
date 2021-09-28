using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUCache
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestMethod()
        {
            LRUCache<string> lruCache = new LRUCache<string>(3);

            lruCache.Put("k1", "v1");
            lruCache.Put("k2", "v2");
            lruCache.Get("k2");
            lruCache.Get("k2");
            lruCache.Get("k2");
            Assert.AreEqual(2, lruCache.Count());
            lruCache.Put("k3", "v3");
            lruCache.Get("k3");
            Assert.AreEqual(3, lruCache.Count());
            Assert.IsTrue(lruCache.ContainsKey("k1"));
            Assert.IsTrue(lruCache.ContainsKey("k2"));
            Assert.IsTrue(lruCache.ContainsKey("k3"));
            lruCache.Put("k4", "v4");
            Assert.AreEqual(3, lruCache.Count());
            Assert.IsFalse(lruCache.ContainsKey("k1"));
            Assert.IsTrue(lruCache.ContainsKey("k2"));
            Assert.IsTrue(lruCache.ContainsKey("k3"));
            Assert.IsTrue(lruCache.ContainsKey("k4"));

        }
    }
}
