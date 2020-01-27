using System;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core.Entities
{
    public class GuestbookEntry : BaseEntity
    {
        public string EmailAddress { get; set; }
        
        public string Message { get; set; }

        public DateTimeOffset DateTimeCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}