using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class ChooseBoatModel : PageModel
    {
        private IBoatRepositoryAsync _repo;
        public List<Boat> Boats { get; set; }
        //public Boat ChosenBoat { get; set; }
        public ChooseBoatModel(IBoatRepositoryAsync boatRepository)
        {
            _repo = boatRepository;
        }
        public async Task OnGet()
        {
            Boats = await _repo.GetAllBoats();
        }
    }
}
