using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class DeleteBookingModel : PageModel
    {
        private IBookingRepositoryAsync _repo;

        public Booking? DeleteBooking { get; set; }
        public DeleteBookingModel(IBookingRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            DeleteBooking = await _repo.SearchBooking(id);
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            Booking? b = await _repo.SearchBooking(id);
            await _repo.RemoveBooking(b);
            return RedirectToPage("index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}

