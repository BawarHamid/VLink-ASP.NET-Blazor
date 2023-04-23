using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database
{
    public class VikarlinkContext : IdentityDbContext<UserEntity>
    {
        public VikarlinkContext(DbContextOptions<VikarlinkContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> AdminDB { get; set; }
        public DbSet<Elev> ElevDB { get; set; }
        public DbSet<KlasseVaerelse> KlassevaerelseDB { get; set; }
        public DbSet<Vagt> VagtDB { get; set; }
        public DbSet<Vikar> VikarDB { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //this will apply configs from separate classes
            //which implemented IEntityTypeConfiguration<T>
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


    }
}
