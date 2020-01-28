using System;
using System.Linq.Expressions;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Core.Specifications
{
    public class GuestbookNotificationPolicy : ISpecification<GuestbookEntry>
    {
        public GuestbookNotificationPolicy(int targetGuestbook, int idOfCreator)
        {
            Criteria = e => e.GuestbookId == targetGuestbook &&
                            e.DateTimeCreated >= DateTimeOffset.UtcNow.AddDays(-1) &&
                            e.Id != idOfCreator;
        }

        public Expression<Func<GuestbookEntry, bool>> Criteria { get; }
    }
}