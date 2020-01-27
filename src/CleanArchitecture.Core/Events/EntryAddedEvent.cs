using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Events
{
    public class EntryAddedEvent : BaseDomainEvent
    {
        public GuestbookEntry targetEntry { get; }
        public int targetParentId { get; }

        public EntryAddedEvent(GuestbookEntry guestbookEntry, int guestbookId)
        {
            targetEntry = guestbookEntry;
            targetParentId = guestbookId;
        }
    }
}