using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 角色的数据传输对象
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// 主键(guid类型)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 角色名字 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }

    }
}
