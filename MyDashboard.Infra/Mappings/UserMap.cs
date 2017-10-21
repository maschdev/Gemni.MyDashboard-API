using MyDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyDashboard.Infra.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            ToTable("UserDashboard");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasColumnName("Name").IsRequired().HasMaxLength(20);
            Property(x => x.ProfileId).HasColumnName("ProfileId").IsRequired(); 
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(32).IsFixedLength();
            HasMany(x => x.Dashboards).WithOptional(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
