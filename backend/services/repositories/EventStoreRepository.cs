using System;
using System.Collections.Generic;
using System.Text;
using core.events;
using Newtonsoft.Json;
using core.seedwork.interfaces;
using entities.logs;

namespace services.repositories
{
    public class EventStoreRepository : IEventStore
    {
        private readonly StoredEventRepository _eventStoreRepository;
        private readonly IUser _user;

        public EventStoreRepository(StoredEventRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public async void Save<T>(T theEvent) where T : Event
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(theEvent);

                var storedEvent = new StoredEvent
                {
                    AggregateId = theEvent.AggregateId,
                    AggregateEntity = theEvent.GetType().FullName,
                    MessageType = theEvent.MessageType,
                    Data = serializedData,
                    Usuario = _user.Name
                };

                await _eventStoreRepository.CreateAsync(storedEvent);
                await _eventStoreRepository.CommitAsync();
            }
            catch (Exception ex) {

                
            }
        }
    }
}
