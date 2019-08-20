using System;

namespace EventAggregatorRx
{
    public static class EventAggregatorExtension
    {
        public static void Publish<TEvent>(this IEventAggregator eventAggregator) where TEvent : IEvent, new() 
            => eventAggregator.Publish(new TEvent());

        public static void Publish<TEvent>(this IEventAggregator eventAggregator, Action<TEvent> action) where TEvent : IEvent, new()
        {
            if (action == null) throw new ArgumentNullException($"Action cannot be null");

            var @event = new TEvent();
            action?.Invoke(@event);
            eventAggregator.Publish(@event);
        }
    }
}