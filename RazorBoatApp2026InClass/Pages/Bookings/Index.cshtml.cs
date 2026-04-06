using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IBookingRepositoryAsync _repo;
        public List<Booking> Bookings { get; set; }
        public IndexModel(IBookingRepositoryAsync repo)
        {
            _repo = repo;
        }
        public async Task OnGet()
        {
            Bookings = await _repo.GetAllBookings();
        }

    }
}
