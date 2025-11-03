using System;

namespace Validosik.Core.Buckets
{
    public interface IBroadcastHandler<T>: IBroadcastHandler where T : struct, IBroadcast
    {
        void Subscribe(Action<T> handler);

        void Unsubscribe(Action<T> handler);

        void Invoke(in T packet);
    }

    public interface IBroadcastHandler { }
}