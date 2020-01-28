using System.Linq;
using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repo)
        {
            _repository = repo;
        }
        
        public IActionResult Index()
        {
            // Seed the database, if no records are found. Should move somewhere useful - i.e. program.cs or similar
            if (!_repository.List<Guestbook>().Any())
            {
                DatabasePopulator.PopulateGuestBookEntries(_repository);
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
            relevantGuestbook.AddEntry(model.NewEntry.FromDTO());
            _repository.Update(relevantGuestbook);

            return View(new HomePageViewModel(relevantGuestbook));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
