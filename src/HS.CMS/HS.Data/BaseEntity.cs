using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data
{
    public class BaseEntity<T>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public T Id { get; set; }
    }
}
