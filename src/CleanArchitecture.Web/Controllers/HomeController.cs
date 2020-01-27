using System;
using System.Collections.Generic;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var hardcodedGuestbook = new Guestbook {Name = "Temporary Guestbook"};
            hardcodedGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from compile time",
                DateTimeCreated = DateTime.UtcNow
            });
            hardcodedGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from the past",
                DateTimeCreated = DateTime.UtcNow.AddHours(-1)
            });
            hardcodedGuestbook.Entries.Add(new GuestbookEntry
            {
                EmailAddress = "ddd-session@ndc.london",
                Message = "Hi from the future - blame the time cast pod machine wibbley wobbley-ness",
                DateTimeCreated = DateTime.UtcNow.AddHours(1)
            });;
            return View(new HomePageViewModel(hardcodedGuestbook.Name, hardcodedGuestbook.Entries));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
