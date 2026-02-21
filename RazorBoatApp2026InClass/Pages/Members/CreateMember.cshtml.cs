using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepository _repo;

        [BindProperty]
        public Member NewMember { get; set; }

        public CreateMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }
        public void OnGet()
        {
        }
        //public IActionResult OnPost()
        //{
        //    _repo.AddMember(NewMember);
        //    return RedirectToPage("index");
        //}
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _repo.AddMember(NewMember);
            }
            catch(MemberPhoneNumberExistsException mEx)
            {
                ViewData["ErrorMessage"] = mEx.Message;
                return Page();
            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
            return RedirectToPage("index");
        }
    }
}
