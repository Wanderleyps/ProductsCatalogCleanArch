using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsCatalogCleanArch.Domain.Entities;

namespace ProductsCatalogCleanArch.Infra.Data.EntitiesConfiguration
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            builder.HasData(
              new Category(1, "Material Escolar"),
              new Category(2, "Eletrônicos"),
               new Category(3, "Acessórios")
            );
        }
    }
}
