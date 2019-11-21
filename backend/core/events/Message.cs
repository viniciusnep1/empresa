using System;
using MediatR;
using core.seedwork;

namespace core.events
{
    public abstract class Message : IRequest<Response>
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
