using System;
using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 角色及其对应的模块类
    /// </summary>
    public class RoleModuleDTO
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
        /// 模块主键(guid类型)
        /// </summary>
        public string ModuleId { get; set; }
        /// <summary>
        /// 模块名字
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 模块描述
        /// </summary>
        public string ModuleDescribe { get; set; }
        /// <summary>
        /// 页面中的控制器名字
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 页面中的action名字
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 模块是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }
    }
}
