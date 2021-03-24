using System;
using System.Web;

namespace SprocketCache
{
    public class SprocketCache : ISprocketCache
    {

        private static readonly object CacheLockObject = new object();
        private readonly ISprocketFactory sprocketFactory;

        public SprocketCache(ISprocketFactory sprocketFactory)
        {
            this.sprocketFactory = sprocketFactory;
        }

        public Sprocket Get(string key)
        {
            var result = HttpRuntime.Cache[key] as Sprocket;
            if (result == null)
            {
                lock (CacheLockObject)
                {
                    result = HttpRuntime.Cache[key] as Sprocket;
                    if (result == null)
                    {
                        result = new Sprocket();
                        HttpRuntime.Cache.Insert(key, result, null, DateTime.Now.AddSeconds(2), TimeSpan.Zero);
                    }
                }
            }
            return result;
        }
    }
}