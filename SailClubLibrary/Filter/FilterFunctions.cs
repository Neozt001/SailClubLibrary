using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Filter
{
    public class FilterFunctions
    {
        public List<T> FilterList<T>(List<T> mList, List<Predicate<T>> pList)
        {
            List<T> resList = mList;
            foreach(Predicate<T> p in pList)
            {
                resList = resList.FindAll(p);
            }
            return resList;
        }
    }
}
