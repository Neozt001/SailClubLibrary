using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages
{
    public class WelcomeModel(IMemberRepositoryAsync memberRepository) : PageModel
    {
        private IMemberRepositoryAsync _repo = memberRepository;
        public string PhoneNumber { get; set; }
        public Member TheMember { get; set; }

        public async Task<IActionResult> OnGet()
        {
            PhoneNumber = HttpContext.Session.GetString("PhoneNumber");
            if (PhoneNumber == null)
            {
                return RedirectToPage("Members/Login");
            }
            else
            {
                TheMember = await _repo.SearchMemberByPhone(PhoneNumber);
            }
            return Page();
        }
    }
}
