using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepositoryAsync _repo;
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Member MemberToUpdate { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }
        //public string MemberPhone { get; set; }
        public EditMemberModel(IMemberRepositoryAsync repo, IWebHostEnvironment webHost)
        {
            _repo = repo;
            webHostEnvironment = webHost;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            MemberToUpdate = await _repo.SearchMember(id);
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            if (Photo != null)
            {
                if (MemberToUpdate.Image != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images/MemberImages", MemberToUpdate.Image);
                    System.IO.File.Delete(filePath);
                }

                MemberToUpdate.Image = ProcessUploadedFile();
            }
            await _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("index");
        }
        public IActionResult OnPostDelete()
        {
            return RedirectToPage("Index");
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
    }
}
