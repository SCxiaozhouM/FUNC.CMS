using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data.Command.Menu
{
    /// <summary>
    /// 添加菜单
    /// </summary>
    public class AddMenuCommand
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
