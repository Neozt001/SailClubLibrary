using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IBoatRepositoryAsync
    {
        #region Properties
        public Task<int> Count { get; }
        #endregion

        #region Methods
        Task<List<Boat>> GetAllBoats();
        Task AddBoat(Boat boat);
        Task RemoveBoat(int id);
        Task UpdateBoat(Boat boat);
        Task<Boat?> SearchBoat(string sailNumber);
        Task<List<Boat>> FilterBoats(string filterCriteria);
        #endregion
    }
}
