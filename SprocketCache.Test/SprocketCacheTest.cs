using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SprocketCache.Test
{
    [TestFixture]
    public class SprocketCacheTest
    {
        [Test]
        public void CanGetSprocket()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            var sprocket = cache.Get("test");
            Assert.IsInstanceOf<Sprocket>(sprocket);
        }

        [Test]
        public void SameKeyReturnsSameSprocket()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            var sprocket1 = cache.Get("test");
            var sprocket2 = cache.Get("test");
            Assert.AreSame(sprocket1, sprocket2, "not the same");
        }

        [Test]
        public void DifferentKeyReturnsDifferentSprocket()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            var sprocket1 = cache.Get("test1");
            var sprocket2 = cache.Get("test2");
            Assert.AreNotSame(sprocket1, sprocket2, "are the same");
        }

        [Test]
        public void Expiry()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            var sprocket1 = cache.Get("test");
            System.Threading.Thread.Sleep(3000);
            var sprocket2 = cache.Get("test");
            Assert.AreNotSame(sprocket1, sprocket2, "are the same");
        }

        [Test]
        public void IsThreadSafe_ParallelDictionary()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            ConcurrentDictionary<Sprocket, int> dict = new ConcurrentDictionary<Sprocket, int>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    dict.TryAdd(cache.Get("test"), i);
                }));
            }

            Task.WhenAll(tasks.ToArray());
            Assert.IsTrue(dict.Keys.Count == 1);
        }

        [Test]
        public void IsThreadSafe_CurrentBag()
        {
            var cache = new SprocketCache(new TestSprocketFactory());
            ConcurrentBag<Sprocket> sprockets = new ConcurrentBag<Sprocket>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    sprockets.Add(cache.Get("test"));
                }));

            }

            Task.WhenAll(tasks.ToArray());
            Assert.That(sprockets.Distinct().Count() == 1);
        }
    }
}