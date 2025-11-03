using System;
using System.Collections.Generic;

namespace Validosik.Core.Buckets
{
    public class Bucket<T> : IBucket where T : struct, IBroadcast
    {
        protected readonly Dictionary<ushort, BroadcastHandler<T>> Subscribes =
            new Dictionary<ushort, BroadcastHandler<T>>();

        public void Subscribe(Action<T> readerDelegate)
        {
            var key = BroadcastHelper.GetKey<T>();
            if (!Subscribes.TryGetValue(key, out var broadcastHandler))
            {
                broadcastHandler = Subscribes[key] = new BroadcastHandler<T>();
            }

            broadcastHandler.Subscribe(readerDelegate);
        }

        public void Unsubscribe(Action<T> readerDelegate)
        {
            var key = BroadcastHelper.GetKey<T>();
            if (Subscribes.TryGetValue(key, out var broadcastHandler))
            {
                broadcastHandler.Unsubscribe(readerDelegate);
            }
        }

        public void Invoke(in T packet)
        {
            var key = BroadcastHelper.GetKey<T>();
            if (Subscribes.TryGetValue(key, out var broadcastHandler))
            {
                broadcastHandler.Invoke(packet);
            }
        }
    }
}