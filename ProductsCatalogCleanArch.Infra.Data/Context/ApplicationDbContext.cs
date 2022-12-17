using Microsoft.EntityFrameworkCore;
using ProductsCatalogCleanArch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCatalogCleanArch.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        //Mapeamento
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //método permite configurar o modelo usando a Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //utlizando para refenciar automaticamente todas as entidades criadas nas classes de EntitiesConfigurations
            //desse forma não precisa instanciar um por um (ex. builder.ApplyConfigurations(new ProductConfiguation();)
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
