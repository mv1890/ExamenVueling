using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> Get();
    }
}
