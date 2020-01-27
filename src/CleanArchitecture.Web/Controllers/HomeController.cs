using System;
using System.Linq;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.SharedKernel.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repo, IMessageSender messageSender)
        {
            _repository = repo;
        }
        
        public IActionResult Index()
        {
            // Seed the database, if no records are found. Should move somewhere useful - i.e. program.cs or similar
            if (!_repository.List<Guestbook>().Any())
            {
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
                _repository.Add(seededGuestbook);
            }
            return View(new HomePageViewModel(_repository.GetById<Guestbook>(1, "Entries")));
        }

        [HttpPost]
        public IActionResult Index(HomePageViewModel model)
        {
            // Needs validation attributes? Not listed in Lab 2, though.
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var relevantGuestbook = _repository.GetById<Guestbook>(1, "Entries");
            relevantGuestbook.AddEntry(model.NewEntry);
            _repository.Update(relevantGuestbook);

            return View(new HomePageViewModel(relevantGuestbook));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
