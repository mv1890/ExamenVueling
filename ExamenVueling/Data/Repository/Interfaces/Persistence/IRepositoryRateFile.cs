using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Interfaces.Persistence
{
    public interface IRepositoryRateFile : IRepositoryFile<RateModel>,  IRepositoryFileWriter<RateModel>, IRepositoryFileReader<RateModel>
    {
    }
}
