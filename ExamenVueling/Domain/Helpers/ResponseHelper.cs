using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helpers
{
    public class ResponseHelper<T>
    {
        public T DataH { get; set; }
        public List<T> ListDataH { get; set; }
        public ResultHelper ResponseH { get; set; }
        public String TextH { get; set; }
        public Decimal DecimalValueH { get; set; }
    }

    public class ResponseHelperDto
    {
        public ResultHelper ResponseH { get; set; }
    }
}
