using System;
using MediatR;
using core.seedwork;

namespace core.commands
{
    public abstract class Command : IRequest<Response>
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}
