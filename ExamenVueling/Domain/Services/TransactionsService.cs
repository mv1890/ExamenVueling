using Data.Repository.Interfaces;
using Data.Repository.Interfaces.Persistence;
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
        private readonly IRepositoryTransFile _repositoryTransFile;
        private readonly IRepositoryRateFile _repositoryRateFile;

        public TransactionsService(IRepository<RateModel> repositoryRate, IRepository<TransactionModel> repositoryTransaction, IRepositoryRateFile repositoryRateFile,
            IRepositoryTransFile repositoryTransFile)
        {
            _repositoryRate = repositoryRate;
            _repositoryTransaction = repositoryTransaction;
            _repositoryRateFile = repositoryRateFile;
            _repositoryTransFile = repositoryTransFile;
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

                _repositoryTransFile.Save(listTransaction);
                _repositoryRateFile.Save(listRate);

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.ResponseH.Result = ResultMsg.Exception;
                response.ResponseH.ErrText = ex.Message;
                return await Task.FromResult(response);
            }
        }

        public Task<ResponseHelper<TransHelper>> GetTransactionsBySku(string sku)
        {
            throw new NotImplementedException();
        }
    }
}
