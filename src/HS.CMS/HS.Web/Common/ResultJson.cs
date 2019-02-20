using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS.Web.Common
{
    public class ResultJson
    {
        public ResultState State { get; set; }
    }

    public enum ResultState
    {

        /// <summary>
        ///  错误异常
        /// </summary>
        Error = -1,
        /// <summary>
        ///  正确
        /// </summary>
        OK = 1,
    }
}
