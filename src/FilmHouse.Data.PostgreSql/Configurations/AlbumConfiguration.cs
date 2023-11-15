using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(e => new { e.AlbumId }).HasName("album_ix00");

        builder.ToTable("Album");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.AlbumId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Title)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UserId)
            .HasColumnType("uuid");

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Item)
            .HasColumnType("text");

        builder.Property(e => e.Summary)
            .HasColumnType("text");

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("timestamp(3)");

    }
}