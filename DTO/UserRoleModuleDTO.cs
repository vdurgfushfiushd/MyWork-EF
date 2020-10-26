using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 用户拥有的角色名，模块名
    /// </summary>
    public class UserRoleModuleDTO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户对应的角色名
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 用户对应的模块名
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 页面中的控制器名字
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 页面中的action名字
        /// </summary>
        public string ActionName { get; set; }

    }
}
