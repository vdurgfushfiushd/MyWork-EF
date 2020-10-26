using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 页面模块表
    /// </summary>
    public class Module: Entity
    {
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
        public string ControllerName{ get; set; }
        /// <summary>
        /// 页面中的action名字
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
