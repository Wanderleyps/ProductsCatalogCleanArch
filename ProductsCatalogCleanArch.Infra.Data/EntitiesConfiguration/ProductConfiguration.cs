using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsCatalogCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Infra.Data.EntitiesConfiguration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

            //definindo precisão do atributo, 10 posições e 2 casas decimais
            builder.Property(p => p.Price).HasPrecision(10, 2);

            //configura um relacionamento um para muitos entre Category e Products
            builder.HasOne(e => e.Category).WithMany(e => e.Products)
                .HasForeignKey(e => e.CategoryId);
        }
    }
}