using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public async Task OnGet(int boatId)
        {
            ChosenBoat = await _bRepo.SearchBoat(boatId);
            BoatId = BoatId;
        }

        public async Task<IActionResult> OnPost()
        {

            TheBooking.TheMember = await _mRepo.SearchMember(MemberId);
            //TheBooking.TheMember = _mRepo.SearchMember(PhoneNumber);
            TheBooking.TheBoat = await _bRepo.SearchBoat(BoatId);
            TheBooking.StartDate = StartDate;
            TheBooking.EndDate = EndDate;
            await _repo.AddBooking(TheBooking);
            return RedirectToPage("Index");
        }
    }
}
