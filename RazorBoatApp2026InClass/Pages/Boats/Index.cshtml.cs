using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.NewFolder
{
    public class IndexModel : PageModel
    {

        private IBoatRepository _bRepo;
        public List<Boat> Boats { get; set; }

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
        }
    }
}
