using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 身高
        /// </summary>
        public decimal? Height { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 用户角色多对多
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
