using MyDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyDashboard.Infra.Mappings
{
    public class DashboardMap : EntityTypeConfiguration<Dashboard>
    {
        public DashboardMap()
        {
            ToTable("Dashboard");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x =>x.Url).HasColumnName("Url").IsRequired();
            Property(x => x.Order).HasColumnName("Order").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId");
        }
    }
}
