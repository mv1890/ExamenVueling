using Data.Repository.Interfaces;
using Data.Repository.Interfaces.Persistence;
using Data.Repository.Models;
using Domain.Helpers;
using Domain.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Data.Utils.Utils;
using static Domain.Helpers.ResultTypeHelper;

namespace Domain.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IRepository<TransactionModel> _repositoryTransaction;
        private readonly IRepository<RateModel> _repositoryRate;
        private readonly IRepositoryTransFile _repositoryTransFile;
        private readonly IRepositoryRateFile _repositoryRateFile;
        private readonly ILogger _logger;

        public TransactionsService(IRepository<RateModel> repositoryRate, IRepository<TransactionModel> repositoryTransaction, IRepositoryRateFile repositoryRateFile,
            IRepositoryTransFile repositoryTransFile, ILogger<TransactionsService> logger)
        {
            _repositoryRate = repositoryRate;
            _repositoryTransaction = repositoryTransaction;
            _repositoryRateFile = repositoryRateFile;
            _repositoryTransFile = repositoryTransFile;
            _logger = logger;
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
                
                if (listTransaction == null && listRate == null)
                {
                    listRate = await _repositoryRateFile.Read();
                    listTransaction = await _repositoryTransFile.Read();
                }

                await _repositoryTransFile.Save(listTransaction);
                await _repositoryRateFile.Save(listRate);

                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.ResponseH.Result = ResultMsg.Exception;
                response.ResponseH.ErrText = ex.Message;
                _logger.LogWarning(DateTime.Now.ToString() + "[" + nameof(TransactionsService) + "]-[" + nameof(Get) + "]: " + ex.Message, ex);
                return await Task.FromResult(response);
            }
        }

        public async Task<ResponseHelper<TransHelper>> GetTransactionsBySku(string sku)
        {
            var response = new ResponseHelper<TransHelper>();
            try
            {
                CurrencyCalc cC = new CurrencyCalc();
                double totalAmount = 0;
                List<TransactionModel> listTransFinal = new List<TransactionModel>();
                List<string> listCoins = new List<string>();
                //API
                Get();

                //FILE
                var listRate = await _repositoryRateFile.Read();
                var listTransaction = await _repositoryTransFile.Read();
                var listTransBySku = listTransaction.Where(x => x.Sku == sku);
                var listFrom = listRate.Select(x => x.From);//FROM
                var listTo = listRate.Select(y => y.To);//TO
                var listFull = listFrom.Union(listTo).Distinct();
                
                foreach (var item in listFull)
                {
                    listCoins.Add(item);
                }
                cC.AddCoin(listCoins);
                foreach (var item in listRate)
                {
                    cC.AddWay(item.From, item.To, Convert.ToSingle(item.Rate));
                }
                
                foreach (var item in listTransBySku)
                {
                    TransactionModel tran = new TransactionModel
                    {
                        Currency = "EUR",
                        Sku = item.Sku
                    };
                    var num = Convert.ToSingle(item.Amount);
                    var dijkstra = cC.Dijkstra("EUR", item.Currency, listFull.Count());
                    tran.Amount = Math.Round((num * dijkstra), 2);
                    totalAmount += (num * dijkstra);
                    listTransFinal.Add(tran);
                }
                    TransHelper finalResponse = new TransHelper
                    {
                        TotalAmount = Math.Round(totalAmount, 2),
                        Current = "€",
                        ListTrans = listTransFinal
                    };
                response.ResponseH = new ResultHelper
                {
                    Result = ResultMsg.OK,
                    ErrText = ""
                };
                response.DataH = finalResponse;
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                response.ResponseH.Result = ResultMsg.Exception;
                response.ResponseH.ErrText = ex.Message;
                _logger.LogWarning(DateTime.Now.ToString() + "[" + nameof(TransactionsService) + "]-[" + nameof(GetTransactionsBySku) + "]: " + ex.Message, ex);
                return await Task.FromResult(response);
            }
        }
    }
}
