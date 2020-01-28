using CleanArchitecture.Core.Entities;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CleanArchitecture.Web
{
    public static class SeedData
    {
        public static readonly ToDoItem ToDoItem1 = new ToDoItem
        {
            Title = "Get Sample Working",
            Description = "Try to get the sample to build."
        };
        public static readonly ToDoItem ToDoItem2 = new ToDoItem
        {
            Title = "Review Solution",
            Description = "Review the different projects in the solution and how they relate to one another."
        };
        public static readonly ToDoItem ToDoItem3 = new ToDoItem
        {
            Title = "Run and Review Tests",
            Description = "Make sure all the tests run and review what they are doing."
        };

        public static readonly Guestbook SeededGuestbook = new Guestbook
        {
            Name = "Temporary Guestbook"
        };
        public static readonly GuestbookEntry CompileTimeEntry = new GuestbookEntry
        {
            EmailAddress = "ddd-session@ndc.london",
            Message = "Hi from compile time",
            DateTimeCreated = DateTime.UtcNow
        };
        public static readonly GuestbookEntry YesterdaysEntry = new GuestbookEntry
        {
            EmailAddress = "ddd-session@ndc.london",
            Message = "Hi from yesterday",
            DateTimeCreated = DateTime.UtcNow.AddDays(-1)
        };
        public static readonly GuestbookEntry OneHourAgoEntry = new GuestbookEntry
        {
            EmailAddress = "ddd-session@ndc.london",
            Message = "Hi from an hour ago",
            DateTimeCreated = DateTime.UtcNow.AddHours(-1)
        };
        public static readonly GuestbookEntry TomorrowsEntry = new GuestbookEntry
        {
            EmailAddress = "ddd-session@ndc.london",
            Message = "Hi from the future - blame the time cast pod machine wibbley wobbley-ness",
            DateTimeCreated = DateTime.UtcNow.AddHours(1)
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
            {
                // Look for any TODO items.
                if (dbContext.ToDoItems.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);


            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var item in dbContext.ToDoItems)
            {
                dbContext.Remove(item);
            }
            dbContext.SaveChanges();
            dbContext.ToDoItems.Add(ToDoItem1);
            dbContext.ToDoItems.Add(ToDoItem2);
            dbContext.ToDoItems.Add(ToDoItem3);

            foreach (var item in dbContext.Guestbooks)
            {
                dbContext.Remove(item);
            }
            
            SeededGuestbook.Entries.Add(CompileTimeEntry);

            dbContext.Guestbooks.Add(SeededGuestbook);

            dbContext.SaveChanges();
        }
    }
}
