using System.Collections.Generic;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Web.ViewModels
{
    public class HomePageViewModel
    {

        // Default constructor provided for object initialisation pattern
        public HomePageViewModel() { }
        
        public HomePageViewModel(string name, List<GuestbookEntry> entries)
        {
            GuestbookName = name;
            PreviousEntries = entries;
        }
        public string GuestbookName { get; set; }
        public List<GuestbookEntry> PreviousEntries { get; } = new List<GuestbookEntry>();
    }
}