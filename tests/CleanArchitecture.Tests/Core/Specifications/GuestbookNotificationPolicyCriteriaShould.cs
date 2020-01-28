using System;
using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Specifications;
using Xunit;

namespace CleanArchitecture.UnitTests.Core.Specifications
{
    public class GuestbookNotificationPolicyCriteriaShould
    {
        private List<GuestbookEntry> TestEntries()
        {
            var entries = new List<GuestbookEntry>
            {
                new GuestbookEntry
                {
                    Id = 1,
                    DateTimeCreated = DateTime.UtcNow,
                    EmailAddress = "test1@test.com",
                    Message = "1",
                    GuestbookId = 1
                },
                new GuestbookEntry
                {
                    Id = 2,
                    DateTimeCreated = DateTime.UtcNow.AddHours(-2),
                    EmailAddress = "test2@test.com",
                    Message = "2",
                    GuestbookId = 1
                },
                new GuestbookEntry
                {
                    Id = 3,
                    DateTimeCreated = DateTime.UtcNow.AddHours(-20),
                    EmailAddress = "test3@test.com",
                    Message = "3",
                    GuestbookId = 2
                },
                new GuestbookEntry
                {
                    Id = 4,
                    DateTimeCreated = DateTime.UtcNow.AddDays(-1).AddSeconds(-1),
                    EmailAddress = "test4@test.com",
                    Message = "4",
                    GuestbookId = 2
                },
                new GuestbookEntry
                {
                    Id = 5,
                    DateTimeCreated = DateTime.UtcNow.AddHours(-10).AddSeconds(-1),
                    EmailAddress = "test4@test.com",
                    Message = "4",
                    GuestbookId = 2
                }
            };
                
            return entries;
        }

        [Fact]
        public void NotIncludeTestEntries()
        {
            var entries = TestEntries();
            var spec = new GuestbookNotificationPolicy(0, 1);

            var filteredEntries = entries.Where(spec.Criteria.Compile());
            
            Assert.Empty(filteredEntries);
        }

        [Fact]
        public void IncludeOneTestEntry()
        {
            var entries = TestEntries();
            var spec = new GuestbookNotificationPolicy(1, 1);
            
            var filteredEntries = entries.Where(spec.Criteria.Compile());
            
            Assert.NotEmpty(filteredEntries);
            Assert.Single(filteredEntries);
        }

        [Fact]
        public void NotIncludeEntriesOverOneDayOld()
        {
            var entries = TestEntries();
            var spec = new GuestbookNotificationPolicy(2, 1);

            var filteredEntries = entries.Where(spec.Criteria.Compile());
            
            Assert.NotEmpty(filteredEntries);
            Assert.Equal(2, filteredEntries.Count());
            Assert.NotEqual(entries.Count, filteredEntries.Count());
        }
    }
}