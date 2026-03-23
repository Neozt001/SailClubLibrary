using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IMemberRepository
    {
        Task<int> Count { get; }
        Task AddMember(Member member);
        Task RemoveMember(Member member);
        Task UpdateMember(Member member);
        Task<List<Member>> GetAllMembers();
        void PrintAll();
        Task<Member?> SearchMember(string phoneNumber);
        Task<List<Member>> FilterMembers(string filterCriteria);
    }
    //public interface IMemberRepository
    //{
    //    int Count { get; }
    //    void AddMember(Member member);
    //    void RemoveMember(Member member);
    //    void UpdateMember(Member member);
    //    List<Member> GetAllMembers();
    //    void PrintAll();
    //    Member? SearchMember(string phoneNumber);
    //    List<Member> FilterMembers(string filterCriteria);
    //}
}
