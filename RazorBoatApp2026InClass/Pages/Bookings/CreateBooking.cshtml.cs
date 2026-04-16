using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingRepositoryAsync _repo;
        private IBoatRepositoryAsync _bRepo;
        private IMemberRepositoryAsync _mRepo;
        [BindProperty]
        public Booking TheBooking { get; set; }
        [BindProperty]
        public string SailNumber { get; set; }
        [BindProperty]
        public Boat ChosenBoat { get; set; }
        [BindProperty]
        public int MemberId { get; set; }
        [BindProperty]
        public int BoatId { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        public CreateBookingModel(IBookingRepositoryAsync repo, IBoatRepositoryAsync bRepo, IMemberRepositoryAsync mRepo)
        {

            _repo = repo;
            _bRepo = bRepo;
            _mRepo = mRepo;
        }
        /// Tidligere ikke IActionResult
        public async Task<IActionResult> OnGet(int boatId)
        {
            try
            {
                ChosenBoat = await _bRepo.SearchBoat(boatId);
                BoatId = BoatId;
            }
            catch (BoatDoesntExistsException ex)
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
            //ChosenBoat = await _bRepo.SearchBoat(boatId);
            //BoatId = BoatId;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                TheBooking.TheMember = await _mRepo.SearchMember(MemberId);
                TheBooking.TheBoat = await _bRepo.SearchBoat(BoatId);
                TheBooking.StartDate = StartDate;
                TheBooking.EndDate = EndDate;
                await _repo.AddBooking(TheBooking);
            }
            catch (MemberDoesntExistsException mEx)
            {
                ViewData["ErrorMessage"] = mEx.Message;
                return Page();
            }
            catch (BoatDoesntExistsException bEx)
            {
                ViewData["ErrorMessage"] = bEx.Message;
                return Page();
            }
            catch (Exception exp)
            {
                ViewData["ErrorMessage"] = exp.Message;
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
