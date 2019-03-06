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
    public class RepositoryTransFile : IRepositoryTransFile
    {
        //RUTA -> ExamenVueling\Api\bin\Debug\netcoreapp2.1
        private static readonly string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"transPersistenceFile.txt");

        public async Task Save(List<TransactionModel> listTrans)
        {
            using (StreamWriter file = new StreamWriter(_path, false))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, listTrans);
            }
        }

        public async Task<List<TransactionModel>> Read()
        {
            using (StreamReader r = new StreamReader(_path))
            {
                string json = r.ReadToEnd();
                //RateModel rate = JsonConvert.DeserializeObject<RateModel>(json);
                List<TransactionModel> listTrans = JsonConvert.DeserializeObject<List<TransactionModel>>(json);
                return listTrans;
            }
        }
    }
}
