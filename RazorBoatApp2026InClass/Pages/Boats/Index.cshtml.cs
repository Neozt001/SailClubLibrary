using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.NewFolder
{
    public class IndexModel : PageModel
    {

        private IBoatRepository _bRepo;
        public List<Boat> Boats { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }



        public IndexModel(IBoatRepository boatRepository)
        {
            _bRepo = boatRepository;
        }
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Boats = _bRepo.FilterBoats(FilterCriteria);
            }
            else
                Boats = _bRepo.GetAllBoats();
            if(!string.IsNullOrEmpty(SortBy))
            {
                SortBoats();
            }
        }

        private void SortBoats()
        {
            if(SortBy == "ID")
            {
                //foreach()
                Boats.Sort();
            }
            else if(SortBy == "SailNumber")
            {
                BoatComparerBySailNumber boatsBySailNumber = new BoatComparerBySailNumber(); 
                Boats.Sort(boatsBySailNumber);
            }
            else if(SortBy == "YearOfConstruction")
            {
                Boats.Sort(new BoatComparerByYear());
            }
        }
    }
}
