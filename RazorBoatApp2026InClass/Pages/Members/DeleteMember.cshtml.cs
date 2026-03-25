using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {
        private IMemberRepositoryAsync _repo;

        public Member? DeleteMember { get; set; }
        public DeleteMemberModel(IMemberRepositoryAsync repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            DeleteMember = await _repo.SearchMember(id);
            return Page();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            Member? m = await _repo.SearchMember(id);
            await _repo.RemoveMember(m);
            return RedirectToPage("index");
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
