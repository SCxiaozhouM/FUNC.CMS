using HS.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace HS.Data.Entities
{
    public class Role:BaseEntity<int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否管理
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 所拥有权限
        /// </summary>
        public string Permission { get; set; }
        /// <summary>
        /// 创建用户id
        /// </summary>
        public long CreateUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public long DelUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DelTime { get; set; }

        /// <summary>
        /// 更改人id
        /// </summary>
        public long UpdateUserId { get; set; }

        /// <summary>
        /// 更改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
