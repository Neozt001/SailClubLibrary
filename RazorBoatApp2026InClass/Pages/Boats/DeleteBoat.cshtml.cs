using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {
        private IBoatRepository _repo;
        public Boat DeleteBoat { get; set; }
        public DeleteBoatModel(IBoatRepository repo)
        {
            _repo = repo;
        }
        public IActionResult OnGet(string sailNumber)
        {
            DeleteBoat = _repo.SearchBoat(sailNumber);
            return Page();
        }

        public  IActionResult OnPostDelete(string sailNumber)
        {
            _repo.RemoveBoat(sailNumber);
            return RedirectToPage("Index");
        }

        public  IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
