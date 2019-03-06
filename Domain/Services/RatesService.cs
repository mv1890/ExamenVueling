using Data.Repository.Interfaces;
using Data.Repository.Models;
using Domain.Helpers;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Domain.Helpers.ResultTypeHelper;

namespace Domain.Services
{
    public class RatesService : IRatesService
    {
        private readonly IRepository<RateModel> _repositoryRate;

        public RatesService(IRepository<RateModel> repositoryRate)
        {
            _repositoryRate = repositoryRate;
        }

        public async Task<ResponseHelper<List<RateModel>>> Get()
        {
            var response = new ResponseHelper<List<RateModel>>();
            try
            {
                var listRate = await _repositoryRate.Get();

                response.ResponseH = new ResultHelper
                {
                    Result = ResultMsg.OK,
                    ErrText = ""
                };
                response.DataH = listRate;
                
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.ResponseH.Result = ResultMsg.Exception;
                response.ResponseH.ErrText = ex.Message;
                return await Task.FromResult(response);
            }
        }
    }
}
