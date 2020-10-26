using Model;
using System.Data.Entity.ModelConfiguration;

namespace DAL.EntityConfig
{
    public class UserConfig : EntityTypeConfiguration<User>
    {
        /// <summary>
        /// 用户表的配置
        /// </summary>
        public UserConfig()
        {
            this.ToTable("t_users");
            this.HasKey(e=>e.Id);
            //设置用户和角色之间的多对多
            this.HasMany(e => e.Roles).WithMany(e => e.Users).Map(m => m.ToTable("t_userrolerelations").MapLeftKey("UserId").MapRightKey("RoleId"));
        }
    }
}
