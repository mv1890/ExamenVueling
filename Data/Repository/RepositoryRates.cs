using Data.Repository.Interfaces;
using Data.Repository.Models;
using Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryRates : IRepository<RateModel>
    {
        private readonly HttpClient _hc;

        public RepositoryRates(HttpClient hc)
        {
            _hc = hc;
        }
        
        public async Task<List<RateModel>> Get()
        {
            var uri = new Uri(Constants.urlRates);
            var respose = await _hc.GetAsync(uri);
            if (respose.IsSuccessStatusCode)
            {
                var content = await respose.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<RateModel>>(content);
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
