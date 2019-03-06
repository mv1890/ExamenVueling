using Data.Repository.Interfaces.Persistence;
using Data.Repository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class RepositoryRateFile : IRepositoryRateFile
    {
        //RUTA -> ExamenVueling\Api\bin\Debug\netcoreapp2.1
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ratesPersistenceFile.txt");

        public async Task Save(List<RateModel> listRates)
        {
            using (StreamWriter file = new StreamWriter(_path, false))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listRates);
            }
        }

        public async Task<List<RateModel>> Read()
        {
            using (StreamReader r = new StreamReader(_path))
            {
                string json = r.ReadToEnd();
                List<RateModel> listRate = JsonConvert.DeserializeObject<List<RateModel>>(json);
                return listRate;
            }
        }
    }
}
