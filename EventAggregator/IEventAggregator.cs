namespace EventAggregator
{
    public interface IEventAggregator
    {
        void Subscribe(object source);
    }
}