namespace EventAggregatorRx
{
    public interface IEventAggregator
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
        void Subscribe<T>(T source) where T : class;
    }
}