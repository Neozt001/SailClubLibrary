using SailClubLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public class GenericRepositoryAsync<T> : Connection, IGenericRepositoryAsync<T>
    {
        public Task<int> Count => throw new NotImplementedException();

        public Task AddObject(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> FilterObj(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllObj()
        {
            throw new NotImplementedException();
        }

        public Task RemoveObject(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T?> SearchObj(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObj(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
