using Data.Repository.Interfaces;
using Data.Repository.Models;
using Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryTrans : IRepository<TransactionModel>
    {
        private readonly HttpClient _hc;

        public RepositoryTrans(HttpClient hc)
        {
            _hc = hc;
        }
        
        public async Task<List<TransactionModel>> Get()
        {
            var uri = new Uri(Constants.urlTrans);
            var respose = await _hc.GetAsync(uri);
            if (respose.IsSuccessStatusCode)
            {
                var content = await respose.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<TransactionModel>>(content);
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
