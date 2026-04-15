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
        public string Message { get; set; }
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
            if(m != null)
            {
                int? sessionID = HttpContext.Session.GetInt32("ID");
                int? sessionRoleValue = HttpContext.Session.GetInt32("MemberRole");
                bool isAdmin = sessionRoleValue.HasValue && (MemberRole)sessionRoleValue == MemberRole.Admin;
                if (!sessionID.HasValue)
                {
                    Message = "For at slette skal du logge ind";
                    return Page();
                }
                else if (sessionID == m.Id)
                {
                    await _repo.RemoveMember(m);
                    HttpContext.Session.Clear();
                    return RedirectToPage("index");
                }
                else
                {
                    Message = "Du kan ikke slette denne bruger";
                    DeleteMember = m;
                    return RedirectToPage("index");
                }
                
            }
            Message = "Brugeren kunne ikke findes";
            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
