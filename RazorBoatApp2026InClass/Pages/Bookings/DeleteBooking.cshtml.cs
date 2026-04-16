using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
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
            try
            {
                DeleteBooking = await _repo.SearchBooking(id);
            }
            catch (BookingDoesntExistsException ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            catch (Exception exp)
            {
                ViewData["ErrorMessage"] = exp.Message;
                return Page();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                Booking? b = await _repo.SearchBooking(id);
                await _repo.RemoveBooking(b);
            }
            catch (BookingDoesntExistsException ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            catch (Exception exp)
            {
                ViewData["ErrorMessage"] = exp.Message;
                return Page();
            }
            return RedirectToPage("index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}

