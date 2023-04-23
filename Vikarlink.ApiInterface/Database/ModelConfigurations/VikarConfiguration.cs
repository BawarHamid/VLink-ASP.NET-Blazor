using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database.ModelConfigurations
{
    public class VikarConfiguration : IEntityTypeConfiguration<Vikar>
    {
        public void Configure(EntityTypeBuilder<Vikar> entity)
        {
            entity.ToTable("Vikar");
            entity.HasKey(x => x.Id);
            
            entity.Property(a => a.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(a => a.ForNavn)
                .HasColumnName("ForNavn")
                .IsRequired();

            entity.Property(a => a.EfterNavn)
                .HasColumnName("EfterNavn")
                .IsRequired();
            
            entity.Property(a => a.Adresse)
                .HasColumnName("Adresse")
                .IsRequired();
            
            entity.Property(a => a.Telefon)
                .HasColumnName("Telefon")
                .IsRequired();
            
            entity.Property(a => a.Email)
                .HasColumnName("Email")
                .IsRequired();
            
            entity.Property(a => a.Username)
                .HasColumnName("BrugerNavn")
                .IsRequired();
            
            entity.Property(a => a.Password)
                .HasColumnName("AdgangsKode")
                .IsRequired();
            
            //entity.Property(a => a.DatoOprettet)
            //    .HasColumnName("DatoOprettet")
            //    .IsRequired();
            
            //entity.Property(a => a.AnsaettelsesDato)
            //    .HasColumnName("EmploymentDate");
            
            //entity.Property(a => a.OpsigelsesDato)
            //    .HasColumnName("TerminationDate");
            
            entity.Property(a => a.FotoURL)
                .HasColumnName("FotoURL");

            //vikar har 1 vagt og en vagt har mange vikarer.
            entity.HasMany(x => x.Vagt)
                .WithOne(x => x.Vikar)
                .OnDelete(DeleteBehavior.Cascade);
            


        }
    }
}
