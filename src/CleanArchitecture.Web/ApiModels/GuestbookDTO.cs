using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Web.ApiModels
{
    public class GuestbookDTO
    {
        public int Id { get; set; }
        public List<GuestbookEntryDTO> Entries { get; set; }

        // If not present, the runtime will try to pick a suitable constructor and will pick the
        // overloaded on below.
        public GuestbookDTO()
        {
            
        }

        public GuestbookDTO(Guestbook gb)
        {
            Id = gb.Id;
            Entries = gb.Entries.Select(e => new GuestbookEntryDTO(e)).ToList();
        }

        public Guestbook FromDTO()
        {
            var gb = new Guestbook()
            {
                Id = this.Id
            };

            foreach (var entry in this.Entries)
            {
                gb.AddEntry(entry.FromDTO());
            }

            return gb;
        }
    }
}