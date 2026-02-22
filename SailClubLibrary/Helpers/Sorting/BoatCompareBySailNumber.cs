using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Sorting
{
    public class BoatComparerBySailNumber : IComparer<Boat>
    {
        public int Compare(Boat? boat1, Boat? boat2)
        {
            if (boat1 == null && boat2 == null) return 0;
            if (boat1 == null) return -1;
            if (boat2 == null) return 1;

            return boat1.SailNumber.CompareTo(boat2.SailNumber);
        }
    }
}
