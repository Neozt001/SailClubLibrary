using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepositoryAsync _repo;
        [BindProperty]
        public Member MemberToUpdate { get; set; }

        //public string MemberPhone { get; set; }
        public EditMemberModel(IMemberRepositoryAsync repo)
        {
            _repo = repo;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            MemberToUpdate = await _repo.SearchMember(id);
            return Page();
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            await _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("index");
        }
        public IActionResult OnPostDelete()
        {
            return RedirectToPage("Index");
        }
    }
}
