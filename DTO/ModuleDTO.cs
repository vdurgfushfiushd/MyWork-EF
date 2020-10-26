using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 模块的数据传输对象
    /// </summary>
    public class ModuleDTO
    {
        /// <summary>
        /// 主键(guid类型)
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 页面名字
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 页面中的控制器名字
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 页面中的action名字
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }
    }
}
