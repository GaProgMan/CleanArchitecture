using System;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Events;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel.Interfaces;

namespace CleanArchitecture.Core.Handlers
{
    public class GuestbookNotificationHandler : IHandle<EntryAddedEvent>
    {
        private readonly IMessageSender _messageSender;
        private readonly IRepository _repository;

        public GuestbookNotificationHandler(IMessageSender messageSender, IRepository repository)
        {
            _messageSender = messageSender;
            _repository = repository;
        }
        public async Task Handle(EntryAddedEvent domainEvent)
        {
            var relevantGuestbook = _repository.GetById<Guestbook>(domainEvent.targetParentId, "Entries");

            var addresses =
                relevantGuestbook.Entries
                    .Where(entry => entry.DateTimeCreated >= DateTimeOffset.UtcNow.AddDays(-1))
                    .Where(entry => entry.Id != domainEvent.targetEntry.Id)
                    .Select(entry => entry.EmailAddress);
            
            foreach (var singleAddress in addresses)
            {
                var messageBody = $"{domainEvent.targetEntry.EmailAddress} left a new message: {domainEvent.targetEntry.Message}";
                _messageSender.SendGuestbookNotificationEmail(singleAddress, messageBody);
            }
        }
    }
}