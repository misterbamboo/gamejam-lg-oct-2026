using System;
using System.Collections.Generic;

namespace Assets.Shared
{
    // Lets domains talk through string events without referencing each other.
    public static class EventBus
    {
        private static readonly Dictionary<string, List<Delegate>> Handlers = new Dictionary<string, List<Delegate>>();

        public static void Subscribe<T>(string eventName, Action<T> handler)
        {
            if (!Handlers.TryGetValue(eventName, out var handlers))
            {
                handlers = new List<Delegate>();
                Handlers[eventName] = handlers;
            }

            handlers.Add(handler);
        }

        public static void Unsubscribe<T>(string eventName, Action<T> handler)
        {
            if (Handlers.TryGetValue(eventName, out var handlers))
            {
                handlers.Remove(handler);
            }
        }

        // Synchronous: every handler has run when Publish returns.
        public static void Publish<T>(string eventName, T payload)
        {
            if (!Handlers.TryGetValue(eventName, out var handlers))
            {
                return;
            }

            // Copy so handlers can subscribe or unsubscribe while being notified.
            foreach (var handler in handlers.ToArray())
            {
                if (handler is Action<T> typedHandler)
                {
                    typedHandler(payload);
                }
            }
        }
    }
}
