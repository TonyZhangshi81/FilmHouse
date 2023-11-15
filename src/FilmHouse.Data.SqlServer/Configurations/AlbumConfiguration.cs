using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(e => new { e.AlbumId });
        builder.HasAnnotation("SqlServer:Name", "album_ix00");

        builder.ToTable("Album");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.AlbumId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Title)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UserId)
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Item)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Summary)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime");
    }
}