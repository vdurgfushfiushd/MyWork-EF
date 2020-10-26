using System;

namespace DTO
{
    /// <summary>
    /// 日志类数据传输对象
    /// </summary>
    public class NoteDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 笔记名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 笔记内容
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool IsDeleted { get; set; } = false;
        /// <summary>
        /// 笔记作者id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 笔记作者
        /// </summary>
        public string Author { get; set; }
    }
}
