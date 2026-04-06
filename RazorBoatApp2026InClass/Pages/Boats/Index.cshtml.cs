using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.NewFolder
{
    public class IndexModel : PageModel
    {

        private IBoatRepositoryAsync _repo;
        public List<Boat> Boats { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }



        public IndexModel(IBoatRepositoryAsync boatRepository)
        {
            _repo = boatRepository;
        }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Boats = await _repo.FilterBoats(FilterCriteria);
            }
            else
                Boats = await _repo.GetAllBoats();
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
