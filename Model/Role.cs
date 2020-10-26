using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        /// <summary>
        /// 多对多
        /// </summary>
        public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
    }
}
