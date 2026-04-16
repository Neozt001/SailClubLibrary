using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBoatApp2026InClass.Helpers;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepositoryAsync _repo;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Boat BoatToUpdate { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public EditBoatModel(IBoatRepositoryAsync repo, IWebHostEnvironment webHost)
        {
            _repo = repo;
            _webHostEnvironment = webHost;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                BoatToUpdate = await _repo.SearchBoat(id);
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
        }

        public async Task<IActionResult> OnPostUpdate()
        {
            string theImage = BoatToUpdate.Image;
            if (Photo != null)
            {
                if (BoatToUpdate.Image != null && BoatToUpdate.Image != Constants.DefaultBoatImage)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/MemberBoatImages", BoatToUpdate.Image);
                    System.IO.File.Delete(filePath);
                }

                theImage = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath, Constants.DefaultBoatImage);
            }
            else
            {
                if (string.IsNullOrEmpty(theImage))
                {
                    theImage = Constants.DefaultBoatImage;
                }
            }
            BoatToUpdate.Image = theImage;
            try
            {
                await _repo.UpdateBoat(BoatToUpdate);
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
        //public async Task<IActionResult> OnPostUpdate()
        //{
        //    await _repo.UpdateBoat(BoatToUpdate);
        //    return RedirectToPage("Index");
        //}
        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                await _repo.RemoveBoat(BoatToUpdate.Id);
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
    }
}
