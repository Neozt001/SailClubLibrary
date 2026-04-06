using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {
        private IBoatRepositoryAsync _repo;
        public Boat? DeleteBoat { get; set; }
        public DeleteBoatModel(IBoatRepositoryAsync repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            DeleteBoat = await _repo.SearchBoat(id);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            await _repo.RemoveBoat(id);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
