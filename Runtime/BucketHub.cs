using System;
using System.Collections.Generic;

namespace Validosik.Core.Buckets
{
    public class BucketHub
    {
        private static readonly Dictionary<Type, object> _buckets = new();

        public static Bucket<T> GetOrCreate<T>() where T : struct, IBroadcast
        {
            if (!_buckets.TryGetValue(typeof(T), out var obj))
            {
                obj = new Bucket<T>();
                _buckets[typeof(T)] = obj;
            }

            return (Bucket<T>)obj;
        }

        public static void Publish<T>(in T packet) where T : struct, IBroadcast
        {
            GetOrCreate<T>().Invoke(packet);
        }
    }
}