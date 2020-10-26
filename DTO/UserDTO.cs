using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    /// <summary>
    /// 用户的数据传输对象
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// 主键（guid类型）
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
        /// 身高
        /// </summary>
        public decimal? Height { get; set; }
        /// <summary>
        /// 创建时间
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
        /// 是否被选择
        /// </summary>
        public bool IsChoosed { get; set; }
    }
}
