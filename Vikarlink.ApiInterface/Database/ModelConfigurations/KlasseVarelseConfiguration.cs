using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vikarlink.ApiInterface.Models;

namespace Vikarlink.ApiInterface.Database.ModelConfigurations
{
    public class KlasseVarelseConfiguration : IEntityTypeConfiguration<KlasseVaerelse>
    {
        public void Configure(EntityTypeBuilder<KlasseVaerelse> entity)
        {
            entity.ToTable("KlasseVaerelse");
            entity.HasKey(x => x.Id);

            entity.Property(a => a.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();
            

            entity.Property(a => a.LokaleInfo)
                .HasColumnName("LokaleInfo")
                .IsRequired();

        }
    }
}
