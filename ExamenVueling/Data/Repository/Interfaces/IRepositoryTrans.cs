using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Interfaces
{
    public interface IRepositoryTrans : IRepository<TransactionModel>
    {
    }
}
