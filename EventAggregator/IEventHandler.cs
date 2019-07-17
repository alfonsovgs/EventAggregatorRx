namespace EventAggregator
{
    public interface IEventHandler<in TEvent>
    {
        void OnHandle(TEvent @event);
    }
}