using Model;
using System.Data.Entity.ModelConfiguration;

namespace DAL.EntityConfig
{
    public class NoteConfig:EntityTypeConfiguration<Note>
    {
        /// <summary>
        /// 日志表的配置
        /// </summary>
        public NoteConfig()
        {
            //设置实体对应的表
            this.ToTable("t_notes");
            //设置主键
            this.HasKey(e=>e.Id);
            //设置日志和用户之间的一对多关系
            this.HasRequired(e => e.User).WithMany().HasForeignKey(e=>e.UserId);
        }
    }
}
