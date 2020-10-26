using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfig
{
    public class ModuleConfig : EntityTypeConfiguration<Module>
    {
        /// <summary>
        /// 模块表的配置
        /// </summary>
        public ModuleConfig()
        {
            //设置实体对应的表
            this.ToTable("t_modules");
            //设置主键
            this.HasKey(e=>e.Id);
        }
    }
}
