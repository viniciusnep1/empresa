using System.Threading.Tasks;
using core.commands;
using core.events;
using MediatR;
using core.seedwork;

namespace core.bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator mediator;
        private readonly IEventStore eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            this.eventStore = eventStore ?? throw new System.ArgumentNullException(nameof(eventStore));
            this.mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            if (@event.MessageType.Equals(nameof(EventLog)))
               eventStore?.Save(@event);

            return mediator.Publish(@event);
        }
    }
}
