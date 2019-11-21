namespace core.events
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T message);
    }
}
