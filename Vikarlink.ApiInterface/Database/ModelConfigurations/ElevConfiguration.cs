using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database.ModelConfigurations
{
    public class ElevConfiguration : IEntityTypeConfiguration<Elev>
    {
        public void Configure(EntityTypeBuilder<Elev> entity)
        {
            entity.ToTable("Elev");
            entity.HasKey(x => x.Id);

            entity.Property(a => a.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(a => a.CprNr)
                .HasColumnName("CprNr")
                .IsRequired();

            entity.Property(a => a.ForNavn)
               .HasColumnName("ForNavn")
               .IsRequired();
            
            entity.Property(a => a.EfterNavn)
               .HasColumnName("EfterNavn")
               .IsRequired();
            
            entity.Property(a => a.Adresse)
               .HasColumnName("Adresse")
               .IsRequired();
            
            entity.Property(a => a.Mobil)
               .HasColumnName("Mobil")
               .IsRequired();
            
            entity.Property(a => a.KontaktNr)
               .HasColumnName("KontaktNr")
               .IsRequired();
            
            entity.Property(a => a.Email)
               .HasColumnName("Email")
               .IsRequired();
            
            entity.Property(a => a.DatoOprettet)
               .HasColumnName("DatoOprettet")
               .IsRequired();

            entity.Property(a => a.FotoURL)
               .HasColumnName("FotoURL");

            //En elev har 1 klassevaerelse og 1 klassevaerelse har mange elever.
            entity.HasOne(x => x.KlasseVaerelse)
                .WithMany(x => x.Elev)
                .HasForeignKey(x => x.KlasseVaerelseId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
