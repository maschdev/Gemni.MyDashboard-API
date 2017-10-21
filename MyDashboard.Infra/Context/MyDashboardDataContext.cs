using MyDashboard.Domain.Entities;
using MyDashboard.Infra.Mappings;
using System.Data.Entity;

namespace MyDashboard.Infra.Context
{
   public class MyDashboardDataContext : DbContext
    {
        //MyDashboardConnectionString
        public MyDashboardDataContext() :
            base(Shared.RunTime.ConnectionString)
            //base(@"Server=(LocalDb)\LocalNT; Integrated Security=true; AttachDbFileName=C:\DB\MyDashboard.mdf;User Id=usergemni;Password=pw23")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new DashboardMap());
           modelBuilder.Configurations.Add(new ProfileMap());
            modelBuilder.Configurations.Add(new UserMap());
        }

        
    }
}
