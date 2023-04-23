using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database.ModelConfigurations
{
    public class VagtConfiguration : IEntityTypeConfiguration<Vagt>
    {
        public void Configure(EntityTypeBuilder<Vagt> entity)
        {
            entity.ToTable("Vagt");
            entity.HasKey(x => x.Id);

            entity.Property(a => a.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(a => a.Dato)
                .HasColumnName("Dato")
                .IsRequired();

            entity.Property(a => a.StartTid)
              .HasColumnName("StartTid")
              .IsRequired();

            entity.Property(a => a.SlutTid)
              .HasColumnName("SlutTid")
              .IsRequired();

            entity.Property(a => a.SlutTid)
              .HasColumnName("SlutTid")
              .IsRequired();

            entity.Property(a => a.Status)
                .HasColumnName("Status")
                .IsRequired();           

            //En Vagt har 1 vikar og vikar har mange vagter.
            entity.HasOne(x => x.Vikar)
                .WithMany(x => x.Vagt)
                .HasForeignKey(x => x.VikarId)
                .OnDelete(DeleteBehavior.Cascade);

            //En Vagt har 1 klasseværelse og et klasseværesle har mange vagter.
            entity.HasOne(x => x.KlasseVaerelse)
                .WithMany(x => x.Vagt)
                .HasForeignKey(x => x.KlasseVaerelseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
