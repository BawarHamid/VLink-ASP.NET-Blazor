using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database.ModelConfigurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
       

        public void Configure(EntityTypeBuilder<Admin> entity)
        {
            entity.ToTable("Admin");
            entity.HasKey(b => b.Id);
            
            entity.Property(a => a.ForNavn)
            .HasColumnName("ForNavn")
            .IsRequired();

            entity.Property(a => a.EfterNavn)
            .HasColumnName("EfterNavn")
            .IsRequired();

            entity.Property(a => a.Telefon)
            .HasColumnName("Telefon")
            .IsRequired();

            entity.Property(a => a.Email)
            .HasColumnName("Email")
            .IsRequired();

            entity.Property(a => a.BrugerNavn)
            .HasColumnName("BrugerNavn")
            .IsRequired();

            entity.Property(a => a.AdgangsKode)
            .HasColumnName("AdgangsKode")
            .IsRequired();

            entity.Property(a => a.DatoOprettet)
            .HasColumnName("DatoOprettet")
            .IsRequired();

            entity.Property(a => a.FotoURL)
            .HasColumnName("FotoURL");
    }
    }
}
