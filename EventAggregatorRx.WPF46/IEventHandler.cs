namespace EventAggregatorRx
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(TEvent @event);
    }
}