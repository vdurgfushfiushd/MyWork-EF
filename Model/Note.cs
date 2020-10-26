using System;

namespace Model
{
    /// <summary>
    /// 日志类
    /// </summary>
    public class Note: Entity
    {
        /// <summary>
        /// 笔记名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 笔记内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 笔记作者id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 笔记和用户的一对多关系
        /// </summary>
        public virtual User User { get; set; }
    }
}
