using log4net;
using Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using Module = Model.Module;

namespace DAL
{
    public class MyDBContext:DbContext
    {
        private static ILog log = LogManager.GetLogger(typeof(MyDBContext));

        public MyDBContext() : base("name=connstr")
        {
            this.Database.Log = (sql) =>
            {
                log.DebugFormat("EF执行SQL:{0}", sql);
            };

            Database.SetInitializer<MyDBContext>(null);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
