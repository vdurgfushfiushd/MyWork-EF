using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfig
{
    public class RoleConfig:EntityTypeConfiguration<Role>
    {
        /// <summary>
        /// 角色表的配置
        /// </summary>
        public RoleConfig()
        {
            //设置实体对应的表
            this.ToTable("t_roles");
            //设置主键
            this.HasKey(e => e.Id);
            //设置角色和模块之间的多对多关系
            this.HasMany(e => e.Modules).WithMany(e => e.Roles).Map(m => m.ToTable("t_rolemodulerelations").MapLeftKey("RoleId").MapRightKey("ModuleId"));
        }
    }
}
