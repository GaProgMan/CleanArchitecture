using System;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Entities
{
    public class GuestbookEntry : BaseEntity
    {
        public int GuestbookId { get; set; }
        public string EmailAddress { get; set; }
        
        public string Message { get; set; }

        public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
    }
}