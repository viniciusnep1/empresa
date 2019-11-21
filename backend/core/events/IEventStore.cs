using core.seedwork;

namespace core.events
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
