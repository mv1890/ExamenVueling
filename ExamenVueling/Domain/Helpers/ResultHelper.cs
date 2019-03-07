using System;
using static Domain.Helpers.ResultTypeHelper;

namespace Domain.Helpers
{
    public class ResultHelper
    {
        public ResultMsg Result { get; set; }
        public String ErrText { get; set; }
    }
}
