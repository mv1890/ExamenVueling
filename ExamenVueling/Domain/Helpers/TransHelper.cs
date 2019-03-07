using Data.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class TransHelper
    {
        public double TotalAmount { get; set; }
        public string Current { get; set; }
        public List<TransactionModel> ListTrans { get; set; }
    }
}
