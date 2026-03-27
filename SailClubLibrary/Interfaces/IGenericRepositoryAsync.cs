using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IGenericRepositoryAsync<T>
    {
        Task<int> Count { get; }
        Task AddObject(T obj);
        Task RemoveObject(T obj);
        Task UpdateObj(T obj);
        Task<List<T>> GetAllObj();
        Task<T?> SearchObj(int id);
        Task<List<T>> FilterObj(string filterCriteria);
    }
}
