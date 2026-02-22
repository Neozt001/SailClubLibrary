using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Globalization;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository _repo;

        public List<Member> Members { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public IndexModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }

        //public void OnGet()
        //{
        //    Members = _repo.GetAllMembers();
        //}
        public void OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Members = _repo.FilterMembers(FilterCriteria);
            }
            else
                Members = _repo.GetAllMembers();
            if (!string.IsNullOrEmpty(SortBy))
            {
                SortMembers();
            }
        }
        private void SortMembers()
        {
            if (SortBy == "ID")
            {
                //foreach()
                Members.Sort();
            }
            else if (SortBy == "FirstName")
            {
                Members.Sort(new MemberComparerByFirstName());
            }
            else if (SortBy == "PhoneNumber")
            {
                Members.Sort(new MemberComparerByPhoneNumber());
            }
            else if (SortBy == "City")
            {
                Members.Sort(new MemberComparerByCity());
            }
        }
    }
}
