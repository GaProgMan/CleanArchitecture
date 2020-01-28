using CleanArchitecture.Core.Entities;
using CleanArchitecture.SharedKernel.Interfaces;
using CleanArchitecture.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Api
{
    [VerifyGuestbookExists]
    public class GuestbookController : BaseApiController
    {
        private readonly IRepository _repository;
        
        public GuestbookController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");

            return Ok(guestbook);
        }

        [HttpPost("{id:int}/NewEntry")]
        public IActionResult NewEntry(int id, [FromBody] GuestbookEntry entry)
        {
            var guestbook = _repository.GetById<Guestbook>(id, "Entries");

            guestbook.Entries.Add(entry);
            _repository.Update(guestbook);
            return Ok(guestbook);
        }
    }
}