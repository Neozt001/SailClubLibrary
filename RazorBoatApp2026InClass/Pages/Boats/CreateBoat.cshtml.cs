using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBoatApp2026InClass.Helpers;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
        private IBoatRepositoryAsync _repo;

        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Boat NewBoat { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public CreateBoatModel(IBoatRepositoryAsync boatRepository, IWebHostEnvironment webHost)
        {
            _repo = boatRepository;
            _webHostEnvironment = webHost;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Photo != null)
            {
                if (NewBoat.Image != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/MemberBoatImages", NewBoat.Image);
                    System.IO.File.Delete(filePath);
                }
                //NewMember.Image = ProcessUploadedFile();
                NewBoat.Image = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath, Constants.DefaultBoatImage);
            }
            else
            {
                //NewMember.Image = ProcessUploadedFile();
                NewBoat.Image = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath, Constants.DefaultBoatImage);
            }
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            try
            {
                await _repo.AddBoat(NewBoat);
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
        //public async Task<IActionResult> OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    try
        //    {
        //        _repo.AddBoat(NewBoat);
                

        //    }
        //    catch (BoatSailnumberExistsException ex)
        //    {
        //        ViewData["ErrorMessage"] = ex.Message;
        //        return Page();
        //    }
        //    catch (Exception exp)
        //    {
        //        ViewData["ErrorMessage"] = exp.Message;
        //        return Page();
        //    }
        //    return RedirectToPage("Index");
        //}
    }
}
