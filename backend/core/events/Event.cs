using System;
using MediatR;

namespace core.events
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; protected set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }

    public abstract class EventLog : Event
    {
        protected EventLog()
        {
            MessageType = nameof(EventLog);
            Timestamp = DateTime.Now;
        }
    }
}
