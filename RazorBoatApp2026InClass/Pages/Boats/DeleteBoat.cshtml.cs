using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
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
            try
            {
                DeleteBoat = await _repo.SearchBoat(id);
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
            //DeleteBoat = await _repo.SearchBoat(id);
            //return Page();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            try
            {
                await _repo.RemoveBoat(id);
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
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
