using Data.Repository.Models;
using Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IRatesService
    {
        Task<ResponseHelper<List<RateModel>>> Get();
    }
}
