using core.bus;
using core.commands;
using core.notifications;
using MediatR;
using System;
using System.Threading.Tasks;

namespace core.seedwork
{
    public class CommandHandler
    {
        //private readonly IMediatorHandler _bus;

        //public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        //{
        //    _bus = bus;
        //}

        //protected void NotifyValidationErrors(Command message)
        //{
        //    foreach (var error in message.ValidationResult.Errors)
        //    {
        //        _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ErrorMessage));
        //    }
        //}

        //public bool Commit()
        //{
        //    if (_notifications.HasNotifications()) return false;
        //    if (_uow.Commit()) return true;

        //    _bus.RaiseEvent(new DomainNotification("Commit", "We had a problem during saving your data."));
        //    return false;
        //}

#pragma warning disable CC0091 // Use static method
        protected async Task<Response> ExecuteAsync(Func<Task<Response>> func)
#pragma warning restore CC0091 // Use static method
        {
            try
            {
                return await func?.Invoke();
            }
            catch (Exception ex)
            {
                return new Response(ex);
            }
        }
    }
}
