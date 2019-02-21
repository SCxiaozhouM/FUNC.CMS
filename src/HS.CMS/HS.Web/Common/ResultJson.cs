using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS.Web.Common
{
    public class ResultJson
    {
        /// <summary>
        /// 状态
        /// </summary>
        public ResultState State { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
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
