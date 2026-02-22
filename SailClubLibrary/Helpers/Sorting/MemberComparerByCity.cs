using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Sorting
{
    public class MemberComparerByCity : IComparer<Member>
    {
        public int Compare(Member? m1, Member? m2)
        {
            if (m1 == null && m2 == null) return 0;
            if (m1 == null) return -1;
            if (m2 == null) return 1;

            return m1.City.CompareTo(m2.City);
        }
    }
}
