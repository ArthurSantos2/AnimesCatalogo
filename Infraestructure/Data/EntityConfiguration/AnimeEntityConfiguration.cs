using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infraestructure.Data.EntityConfiguration
{
    public class AnimeEntityConfiguration : IEntityTypeConfiguration<Anime>
    {
        public void Configure(EntityTypeBuilder<Anime> builder)
        {
            builder.ToTable("Anime");

            builder.HasKey(t => t.Id);

            builder.Property(s => s.AnimeName)
                .HasColumnName("AnimeName");

            builder.Property(s => s.Description)
                .HasColumnName("Description");

            builder.Property(s => s.DirectorName)
                .HasColumnName("DirectorName");

            builder.Property(s => s.IsActive)
                .HasColumnName("IsActive");

        }

    }
}
