using BizCommon_Std.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_UWP
{
    /// <summary>
    /// ViewModel에서 공용으로 사용하는 클래스
    /// </summary>
    public class CommonProperties
    {
        public static CommonProperties Properties
        {
            get
            {
                if (instance == null)
                    instance = new CommonProperties();
                return instance;
            }
        }

        private static CommonProperties instance;

        public ConnectionModel SelectedConnection { get; set; }

    }
}
