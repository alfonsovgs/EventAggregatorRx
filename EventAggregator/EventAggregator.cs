using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace EventAggregator
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Subject<object> _subject = new Subject<object>();

        public void Publish<TEvent>(TEvent @event) => _subject.OnNext(@event);

        public void Subscribe(object source)
        {
            object[] args = { source };
            var eventTypes = source.GetType().GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));

            foreach (var eventType in eventTypes)
            {
                var @event = eventType.GenericTypeArguments.FirstOrDefault();
                var method = typeof(EventAggregator).GetMethod(nameof(SubscribeEventType));
                var generic = method?.MakeGenericMethod(@event);
                generic?.Invoke(this, args);
            }
        }

        public void SubscribeEventType<TEvent>(IEventHandler<TEvent> handler)
        {
            _subject.OfType<TEvent>().AsObservable()
                .Subscribe(handler.OnHandle);
        }
    }
}
