using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepository _repo;
        [BindProperty]
        public Member MemberToUpdate { get; set; }
        public EditMemberModel(IMemberRepository repo)
        {
            _repo = repo;
        }
        public IActionResult OnGet(string phoneNumber)
        {
            MemberToUpdate = _repo.SearchMember(phoneNumber);
            return Page();
        }
        public IActionResult OnPostUpdate()
        {
            _repo.UpdateMember(MemberToUpdate);
            return RedirectToPage("index");
        }
        public IActionResult OnPostDelete()
        {
            return RedirectToPage("Index");
        }
    }
}
