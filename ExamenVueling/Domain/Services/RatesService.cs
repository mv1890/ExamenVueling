using Data.Repository.Interfaces;
using Data.Repository.Interfaces.Persistence;
using Data.Repository.Models;
using Domain.Helpers;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Domain.Helpers.ResultTypeHelper;

namespace Domain.Services
{
    public class RatesService : IRatesService
    {
        private readonly IRepository<RateModel> _repositoryRate;
        private readonly IRepositoryRateFile _repositoryRateFile;
        private readonly ILogger _logger;

        public RatesService(IRepository<RateModel> repositoryRate, IRepositoryRateFile repositoryRateFile, ILogger<RatesService> logger)
        {
            _repositoryRate = repositoryRate;
            _repositoryRateFile = repositoryRateFile;
            _logger = logger;
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

                _repositoryRateFile.Save(listRate);

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.ResponseH.Result = ResultMsg.Exception;
                response.ResponseH.ErrText = ex.Message;
                _logger.LogWarning(DateTime.Now.ToString() + "[" + nameof(RatesService) + "].[" + nameof(Get) + "]: " + ex.Message, ex);
                return await Task.FromResult(response);
            }
        }
    }
}
