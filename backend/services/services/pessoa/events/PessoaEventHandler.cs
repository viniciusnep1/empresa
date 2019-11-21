using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace services.services.pessoa.events
{
    public class PessoaEventHandler : INotificationHandler<PessoaCreatedEvent>
    {
        public Task Handle(PessoaCreatedEvent message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
