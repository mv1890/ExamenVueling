using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces.Persistence
{
    public interface IRepositoryFileReader<T>
    {
        Task<List<T>> Read();
    }
}
