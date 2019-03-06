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
    public class TransactionsService : ITransactionsService
    {
        private readonly IRepository<TransactionModel> _repositoryTransaction;
        private readonly IRepository<RateModel> _repositoryRate;

        public TransactionsService(IRepository<RateModel> repositoryRate, IRepository<TransactionModel> repositoryTransaction)
        {
            _repositoryRate = repositoryRate;
            _repositoryTransaction = repositoryTransaction;
        }

        public async Task<ResponseHelper<List<TransactionModel>>> Get()
        {
            var response = new ResponseHelper<List<TransactionModel>>();
            try
            {
                var listTransaction = await _repositoryTransaction.Get();
                var listRate = await _repositoryRate.Get();

                response.ResponseH = new ResultHelper
                {
                    Result = ResultMsg.OK,
                    ErrText = ""
                };
                response.DataH = listTransaction;

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
