using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingRepository _repo;
        private IBoatRepository _bRepo;
        private IMemberRepository _mRepo;
        [BindProperty]
        public Booking TheBooking { get; set; }
        [BindProperty]
        public Boat ChosenBoat { get; set; }
        //public Member TheMember { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime EndDate { get; set; }
        public CreateBookingModel(IBookingRepository repo, IBoatRepository bRepo, IMemberRepository mRepo)
        {

            _repo = repo;
            _bRepo = bRepo;
            _mRepo = mRepo;
        }
        public void OnGet(string sailNumber)
        {
            ChosenBoat = _bRepo.SearchBoat(sailNumber);
        }

        public IActionResult OnPost()
        {
            TheBooking.TheMember = _mRepo.SearchMember(PhoneNumber);
            TheBooking.TheBoat = ChosenBoat;
            TheBooking.StartDate = StartDate;
            TheBooking.EndDate = EndDate;
            _repo.AddBooking(TheBooking);
            return RedirectToPage("Index");
        }
    }
}
