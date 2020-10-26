using System;
using System.Collections.Generic;

namespace DTO
{
    /// <summary>
    /// 用户及其对应的角色类(用于用户修改页面的显示)
    /// </summary>
    public class UserRoleDTO
    {
        /// <summary>
        /// 用户主键（guid类型）
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户身高
        /// </summary>
        public decimal? Height { get; set; }
        /// <summary>
        /// 用户创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 用户描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 角色主键(guid类型)
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名字 
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescribe { get; set; }
        /// <summary>
        /// 角色是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }
    }
}
