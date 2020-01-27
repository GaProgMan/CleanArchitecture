using System;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel.Interfaces;
using System.Linq;
using CleanArchitecture.SharedKernel;

namespace CleanArchitecture.Core
{
    public static class DatabasePopulator
    {

        public static int PopulateToDos(IRepository repository)
        {
            if (repository.Count<ToDoItem>()>= 3) return 0;

            repository.Add(new ToDoItem
            {
                Title = "Get Sample Working",
                Description = "Try to get the sample to build."
            });
            repository.Add(new ToDoItem
            {
                Title = "Review Solution",
                Description = "Review the different projects in the solution and how they relate to one another."
            });
            repository.Add(new ToDoItem
            {
                Title = "Run and Review Tests",
                Description = "Make sure all the tests run and review what they are doing."
            });

            return repository.Count<ToDoItem>();
        }

        public static int PopulateGuestBookEntries(IRepository repository)
        {
            if (repository.Count<Guestbook>() > 0)
            {
                return repository.Count<Guestbook>();
            }
            
            var seededGuestbook = new Guestbook {Name = "Temporary Guestbook"};
            seededGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from compile time",
                DateTimeCreated = DateTime.UtcNow
            });
            seededGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from yesterday",
                DateTimeCreated = DateTime.UtcNow.AddDays(-1)
            });
            seededGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from an hour ago",
                DateTimeCreated = DateTime.UtcNow.AddHours(-1)
            });
            seededGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from the future - blame the time cast pod machine wibbley wobbley-ness",
                DateTimeCreated = DateTime.UtcNow.AddHours(1)
            });
            repository.Add(seededGuestbook);

            return repository.Count<Guestbook>();
        }
    }
}
