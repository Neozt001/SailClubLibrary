using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Helpers.Sorting
{
    public class BoatComparerByYear : IComparer<Boat>
    {
        public int Compare(Boat? boat1, Boat? boat2)
        {
            if (boat1 == null && boat2 == null) return 0;
            if (boat1 == null) return -1;
            if (boat2 == null) return 1;

            //Convert for accuracy
            int boat1Year = int.Parse(boat1.YearOfConstruction);
            int boat2Year = int.Parse(boat2.YearOfConstruction);
            return boat1Year.CompareTo(boat2Year);
        }
    }
}
