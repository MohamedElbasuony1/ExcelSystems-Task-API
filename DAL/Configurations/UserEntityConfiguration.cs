using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DAL.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> UserBuilder)
        {
            UserBuilder.ToTable("Users");
            UserBuilder.HasKey(a => a.ID);
            UserBuilder.Property(a => a.ID).ForSqlServerUseSequenceHiLo("UserSeq");
            UserBuilder.HasIndex(a => a.UserName).IsUnique();
            UserBuilder.Property(a => a.UserName).IsRequired();
            UserBuilder.Property(a => a.Password).IsRequired();
            UserBuilder.Property(a => a.IsActive).IsRequired();
        }
    }
}
