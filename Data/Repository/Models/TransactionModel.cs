using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository.Models
{
    public class TransactionModel
    {
        public string Sku { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
