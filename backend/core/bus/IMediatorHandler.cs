using System.Threading.Tasks;
using core.commands;
using core.events;

namespace core.bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
