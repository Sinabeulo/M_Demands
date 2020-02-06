using BizCommon_Std.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizCommon_Std.Interface
{
    public interface IFeatureExecute
    {
        ConnectionModel Target { get; set; }
        object Execute();
    }
}
