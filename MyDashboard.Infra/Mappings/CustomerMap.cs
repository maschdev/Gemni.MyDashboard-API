using MyDashboard.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace MyDashboard.Infra.Mappings
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customer");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Document.Number).HasColumnName("Number").IsRequired().HasMaxLength(14).IsFixedLength();
            Property(x => x.Email.Address).HasColumnName("Email").IsRequired().HasMaxLength(60);
            Property(x => x.Name.FirstName).HasColumnName("FirstName").IsRequired().HasMaxLength(60);
            Property(x => x.Name.LastName).HasColumnName("LastName").HasMaxLength(60);
            Property(x => x.Phone).HasColumnName("Phone").IsRequired().HasMaxLength(15);
            Property(x => x.Enable).HasColumnName("Enable").HasColumnType("Bit");

            HasRequired(x => x.User).WithMany().HasForeignKey(o => o.UserId);
            Property(x => x.UserId).HasColumnName("UserId"); // novo
            
        }
    }
}
