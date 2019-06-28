using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;

namespace DAL.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> ProductBuilder)
        {
            ProductBuilder.ToTable("Products");
            ProductBuilder.HasKey(a => a.ID);
            ProductBuilder.Property(a => a.ID).ForSqlServerUseSequenceHiLo("ProSeq");
            ProductBuilder.Property(a => a.Name).IsRequired();
            ProductBuilder.Property(a => a.Price).HasColumnType("decimal(8,3)").IsRequired();
            ProductBuilder.Property(a => a.Price).IsRequired();
            ProductBuilder.Property(a => a.Quantity).IsRequired();

            ProductBuilder
                .HasOne(a => a.User)
                .WithMany(u => u.Products)
                .HasForeignKey(a => a.SupplierId)
                .IsRequired();

        }
    }
}
