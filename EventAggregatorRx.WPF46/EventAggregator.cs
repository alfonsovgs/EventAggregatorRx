using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reflection;

namespace EventAggregatorRx 
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Subject<object> _subject = new Subject<object>();

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent => _subject.OnNext(@event);

        public void Subscribe<T>(T source) where T : class
        {
            object[] args = {source};
            var eventTypes = source.GetType().GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (var eventType in eventTypes)
            {
                var @event = eventType.GenericTypeArguments.FirstOrDefault();
                var method = typeof(EventAggregator).GetMethod(nameof(SubscribeEventType),
                    BindingFlags.NonPublic | BindingFlags.Instance);

                var generic = method?.MakeGenericMethod(@event);
                generic?.Invoke(this, args);
            }
        }

        private void SubscribeEventType<TEvent>(IEventHandler<TEvent> handler)
        {
            _subject.OfType<TEvent>().AsObservable()
                .Subscribe(handler.Handle);
        }
    }
}