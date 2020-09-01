using Equinox.Domain.Core.Core.Messaging;
using Equinox.Domain.Core.Events;
using Equinox.Infra.Data.Repository.EventSourcing;
using NetDevPack.Identity.User;
using Newtonsoft.Json;


namespace Equinox.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IAspNetUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IAspNetUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            // Using Newtonsoft.Json because System.Text.Json
            // is a sad joke and far to be considered "Done"
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                string.IsNullOrWhiteSpace(_user.Name) ? _user.GetUserEmail()  : "Anonymous");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}