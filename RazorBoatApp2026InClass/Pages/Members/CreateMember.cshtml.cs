using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepositoryAsync _repo;

        private IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public Member NewMember { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }


        //public CreateMemberModel(IMemberRepository memberRepository)
        public CreateMemberModel(IMemberRepositoryAsync memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            webHostEnvironment = webHost;
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
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images/MemberImages", NewMember.Image);
                    System.IO.File.Delete(filePath);
                }

                NewMember.Image = ProcessUploadedFile();
            }

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
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
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/MemberImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        //public IActionResult OnPost() //Bruges til at oprette/update/delete
        //{
        //    if (Photo != null)
        //    {
        //        if (NewMember.MemberImage != null)
        //        {
        //            string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/images/MemberImages", NewMember.MemberImage);
        //            System.IO.File.Delete(filePath);
        //        }

        //        NewMember.MemberImage = ProcessUploadedFile();
        //    }
        //    try
        //    {
        //        private string ProcessUploadedFile()
        //        {
        //            string uniqueFileName = null;
        //            if (Photo != null)
        //            {
        //                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages");
        //                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
        //                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    Photo.CopyTo(fileStream);
        //                }
        //        }
        //        return uniqueFileName;
        //     }

    }
}
