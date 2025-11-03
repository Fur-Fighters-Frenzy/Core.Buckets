using System;
using System.Collections.Generic;

namespace Validosik.Core.Buckets
{
    public class BroadcastHandler<T> : IBroadcastHandler<T> where T : struct, IBroadcast
    {
        private readonly List<Action<T>> _subscribes = new List<Action<T>>();

        public void Subscribe(Action<T> handler) =>
            _subscribes.Add(handler);

        public void Unsubscribe(Action<T> handler) =>
            _subscribes.Remove(handler);

        public void Invoke(in T packet)
        {
            var subs = _subscribes.ToArray();
            for (int i = 0, length = subs.Length; i < length; ++i)
            {
                subs[i].Invoke(packet);
            }
        }
    }
}