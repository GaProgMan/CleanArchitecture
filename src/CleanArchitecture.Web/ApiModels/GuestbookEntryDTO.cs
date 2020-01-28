using System;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Web.ApiModels
{
    public class GuestbookEntryDTO
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime DateTimeCreated { get; set; }

        // If not present, the runtime will try to pick a suitable constructor and will pick the
        // overloaded on below.
        public GuestbookEntryDTO()
        {
            
        }

        public GuestbookEntryDTO(GuestbookEntry entry)
        {
            Id = entry.Id;
            EmailAddress = entry.EmailAddress;
            Message = entry.Message;
            DateTimeCreated = entry.DateTimeCreated;
        }

        public GuestbookEntry FromDTO()
        {
            return new GuestbookEntry()
            {
                Id = this.Id,
                EmailAddress = this.EmailAddress,
                Message = this.Message,
                DateTimeCreated = this.DateTimeCreated
            };
        }
    }
}