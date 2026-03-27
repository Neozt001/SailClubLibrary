using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Filter;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Globalization;
using System.Reflection;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepositoryAsync _repo;

        public List<Member> Members { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public MemberType SelectedMemberType { get; set; }

        public IndexModel(IMemberRepositoryAsync memberRepository)
        {
            _repo = memberRepository;
        }
        public async Task OnGet()
        {

            try
            {
                //Members = MemberFilter(_repo.GetAllMembers());
                Members = await _repo.GetAllMembers();

            }
            catch(Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            
            //Members = await _repo.GetAllMembers();
            List<Predicate<Member>> pList = [];
            if (!string.IsNullOrEmpty(FilterBy) && !string.IsNullOrEmpty(FilterCriteria))
            {
                if (FilterBy == "FirstName")
                {
                    pList.Add(m => m.FirstName != null && m.FirstName.Contains(FilterCriteria));
                }
                else if (FilterBy == "SurName")
                {
                    pList.Add(m => m.SurName != null && m.SurName.Contains(FilterCriteria));
                }
                else if (FilterBy == "PhoneNumber")
                {
                    pList.Add(m => m.PhoneNumber != null && m.PhoneNumber.Contains(FilterCriteria));
                }
                else if (FilterBy == "Address")
                {
                    pList.Add(m => m.Address != null && m.Address.Contains(FilterCriteria));
                }
                else if (FilterBy == "City")
                {
                    pList.Add(m => m.City != null && m.City.Contains(FilterCriteria));
                }
                else if (FilterBy == "Mail")
                {
                    pList.Add(m => m.Mail != null && m.Mail.Contains(FilterCriteria));
                }
            }
            if (SelectedMemberType != 0)
            {
                pList.Add(m => m.TheMemberType == SelectedMemberType);
            }
            FilterFunctions filterFunctions = new FilterFunctions();
            Members = filterFunctions.FilterList(Members, pList);
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
