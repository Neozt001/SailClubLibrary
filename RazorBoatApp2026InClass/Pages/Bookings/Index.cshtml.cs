using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IBookingRepository _repo;
        public List<Booking> Bookings { get; set; }
        public IndexModel(IBookingRepository repo)
        {
            _repo = repo;
        }
        public void OnGet()
        {
            Bookings = _repo.GetAllBookings();
        }
    }
}
