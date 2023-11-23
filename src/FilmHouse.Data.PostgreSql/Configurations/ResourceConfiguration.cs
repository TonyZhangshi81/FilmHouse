using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasKey(e => new { e.ResourceId }).HasName("resource_ix00");

        builder.ToTable("Resource");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Name)
            .HasColumnType("text");

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(e => e.Size)
            .HasColumnType("bigint");

        builder.Property(e => e.UserId)
            .HasColumnType("uuid");

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Time)
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(0)
            .HasColumnType("int");

        builder.Property(e => e.Type)
            .HasDefaultValue(0)
            .HasColumnType("smallint");

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(0)
            .HasColumnType("smallint");

        builder.Property(e => e.ReviewNote)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

    }
}