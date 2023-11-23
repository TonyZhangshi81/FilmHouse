using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasKey(e => new { e.ResourceId }).HasName("resource_ix00");

        builder.ToTable("Resource");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Name)
            .HasColumnType("longtext");

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("longtext");

        builder.Property(e => e.Size)
            .HasColumnType("bigint");

        builder.Property(e => e.UserId)
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Time)
            .HasColumnType("datetime(3)");

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(0)
            .HasColumnType("int");

        builder.Property(e => e.Type)
            .HasDefaultValue(0)
            .HasColumnType("tinyint unsigned");

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(0)
            .HasColumnType("tinyint unsigned");

        builder.Property(e => e.ReviewNote)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

    }
}