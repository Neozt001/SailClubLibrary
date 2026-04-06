using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBoatApp2026InClass.Helpers;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.ComponentModel.Design;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {

        private IMemberRepositoryAsync _repo;

        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Member NewMember { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }


        //public CreateMemberModel(IMemberRepository memberRepository)
        public CreateMemberModel(IMemberRepositoryAsync memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            _webHostEnvironment = webHost;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (Photo != null)
            {
                if (NewMember.Image != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/MemberBoatImages", NewMember.Image);
                    System.IO.File.Delete(filePath);
                }
                //NewMember.Image = ProcessUploadedFile();
                NewMember.Image = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath,  Constants.DefaultMemberImage);
            }
            else
            {
                //NewMember.Image = ProcessUploadedFile();
                NewMember.Image = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath, Constants.DefaultMemberImage);
            }
            try
            {

                await _repo.AddMember(NewMember);
            }
            catch (MemberPhoneNumberExistsException mEx)
            {
                ViewData["ErrorMessage"] = mEx.Message;
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("index");
        }
            //private string ProcessUploadedFile()
            //{
            //    string uniqueFileName = null;
            //    if (Photo != null)
            //    {
            //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/MemberImages");
            //        if (!Directory.Exists(uploadsFolder))
            //        {
            //            Directory.CreateDirectory(uploadsFolder);
            //        }
            //        uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
            //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //        using (var fileStream = new FileStream(filePath, FileMode.Create))
            //        {
            //            Photo.CopyTo(fileStream);
            //        }
            //    }
            //    else
            //    {
            //        return "Default.jpg";
            //    }
            //    return uniqueFileName;
            //}
    }
}
