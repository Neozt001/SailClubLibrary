using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBoatApp2026InClass.Helpers;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class EditBookingModel : PageModel
    {
        private IBookingRepositoryAsync _repo;
        [BindProperty]
        public Booking BookingToUpdate { get; set; }


        public EditBookingModel(IBookingRepositoryAsync repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            BookingToUpdate = await _repo.SearchBooking(id);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            await _repo.UpdateBooking(BookingToUpdate);
            return RedirectToPage("index");
        }
    }
}