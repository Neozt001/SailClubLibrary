using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepository _repo;
        [BindProperty]
        public Boat BoatToUpdate { get; set; }
        public EditBoatModel(IBoatRepository repo)
        {
            _repo = repo;
        }
        public void OnGet(string sailNumber)
        {
            BoatToUpdate = _repo.SearchBoat(sailNumber);
        }

        public IActionResult OnPostUpdate()
        {
            _repo.UpdateBoat(BoatToUpdate);
            return RedirectToPage("Index");
        }
        public IActionResult OnPostDelete()
        {
            _repo.RemoveBoat(BoatToUpdate.SailNumber);
            return RedirectToPage("Index");
        }
    }
}
