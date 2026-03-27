using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Data
{
    public class MockData
    {
        #region Instance fields
        private Dictionary<string, Member> _memberData =
            new Dictionary<string, Member>()
            {
            { "23456789", new Member(1, "Peter","Jensen","23456789","Gaden 1","Hillerød","PH@gamil.com",MemberType.Senior,MemberRole.Member, "Default.jpg") },
             { "65345890", new Member(2, "Charlotte","Hansen","65345890","Street 1","Roskilde","ch@gamil.com",MemberType.Adult,MemberRole.Admin, "Default.jpg") },
            };

        private Dictionary<string, Boat> _boatData =
              new Dictionary<string, Boat>()
              {
              { "16-3335", new Boat(1, "16-3335", "Model1", 32, 23, 33, "1982", "Is very good :3", BoatType.TERA)},
              { "17-8767", new Boat(2, "17-8767", "Model2", 34, 25, 17, "2000", "Fast :3", BoatType.TERA)},
              };
        #endregion

        #region Properties
        public Dictionary<string, Member> MemberData
        {
            get { return _memberData; }
        }
        public Dictionary<string, Boat> BoatData
        {
            get { return _boatData; }
        }

        #endregion
    }
}
