using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorBoatApp2026InClass.Helpers;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepositoryAsync _repo;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Member MemberToUpdate { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }
        //public string MemberPhone { get; set; }
        public EditMemberModel(IMemberRepositoryAsync repo, IWebHostEnvironment webHost)
        {
            _repo = repo;
            _webHostEnvironment = webHost;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            MemberToUpdate = await _repo.SearchMember(id);
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            string theImage = MemberToUpdate.Image;
            if (Photo != null)
            {
                if (MemberToUpdate.Image != null && MemberToUpdate.Image != Constants.DefaultMemberImage)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images/MemberBoatImages", MemberToUpdate.Image);
                    System.IO.File.Delete(filePath);
                }

                theImage = ProcessImage.ProcessUploadedFile(Photo, _webHostEnvironment.WebRootPath, Constants.DefaultMemberImage);
            }
            else
            {
                if (string.IsNullOrEmpty(theImage))
                {
                    theImage = Constants.DefaultMemberImage;
                }
            }
            MemberToUpdate.Image = theImage;
            await _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("index");
        }
        public IActionResult OnPostDelete()
        {
            return RedirectToPage("Index");
        }
    }
}
