using System.Collections.Generic;
using System.Linq;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Web.ApiModels;

namespace CleanArchitecture.Web.ViewModels
{
    public class HomePageViewModel
    {

        // Default constructor provided for object initialisation pattern
        public HomePageViewModel() { }
        
        public HomePageViewModel(string name, IEnumerable<GuestbookEntry> entries)
        {
            GuestbookName = name;
            PreviousEntries = entries.Select(e => new GuestbookEntryDTO(e)).ToList();
        }

        public HomePageViewModel(Guestbook dbGuestbook)
        {
            GuestbookName = dbGuestbook.Name;
            PreviousEntries = dbGuestbook.Entries.Select(e => new GuestbookEntryDTO(e)).ToList();
        }
        
        public string GuestbookName { get; set; }
        public List<GuestbookEntryDTO> PreviousEntries { get; } = new List<GuestbookEntryDTO>();

        public GuestbookEntryDTO NewEntry { get; set; }
    }
}