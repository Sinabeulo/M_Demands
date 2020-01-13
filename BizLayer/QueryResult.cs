using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLayer
{
    public interface QueryResult
    {
        ResultStatus resultStatus { get; set; }
    }

    public enum ResultStatus
    {
        X = 0,
        O = 1
    }
}
