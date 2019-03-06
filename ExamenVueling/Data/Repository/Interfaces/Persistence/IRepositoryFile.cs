using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces.Persistence
{
    public interface IRepositoryFile<T>
    {
        Task Save(List<T> list);
        Task<List<T>> Read();
    }
}
