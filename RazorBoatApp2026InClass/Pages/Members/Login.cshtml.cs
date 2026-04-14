using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class LoginModel : PageModel
    {
        private IMemberRepositoryAsync _repo;

        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public LoginModel(IMemberRepositoryAsync memberRepository)
        {
            _repo = memberRepository;
        }
        public void OnGet()
        {
        }

        public void OnGetLogout()
        {
            //HttpContext.Session.Remove("PhoneNumber");
            HttpContext.Session.Clear();
        }

        public async Task<IActionResult> OnPost()
        {
            if(string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Password))
            {
                Message = "Nummer eller kodeordet er ikke indtastet korrekt";
                return Page();
            }

            Member? user = await _repo.VerifyMember(PhoneNumber, Password);
            
            if (user != null)
            {
                HttpContext.Session.SetInt32("ID", user.Id);
                HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
                HttpContext.Session.SetString("Password", user.Password);
                HttpContext.Session.SetInt32("MemberRole", (int)user.TheMemberRole);
                return RedirectToPage("/Welcome");
            }
            else
            {
                Message = "Nummer eller kodeordet er ikke korrekt";
                PhoneNumber = "";
                Password = "";
                return Page();
            }
        }
    }
}
