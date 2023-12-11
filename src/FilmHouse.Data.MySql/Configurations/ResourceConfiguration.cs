using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

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
            .HasMaxLength(36)
            .HasConversion<ResourceIdVO.ResourceIdValueConverter>();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<ResourceNameVO.ResourceNameValueConverter>();

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("varchar(400)")
            .HasMaxLength(400)
            .HasConversion<ResourceContentVO.ResourceContentValueConverter>();

        builder.Property(e => e.Size)
            .HasDefaultValue(typeof(ResourceSizeVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(11)")
            .HasConversion<ResourceSizeVO.ResourceSizeValueConverter>();

        builder.Property(e => e.UserId)
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(typeof(FavorCountVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(4)")
            .HasConversion<FavorCountVO.FavorCountValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(ResourceTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint unsigned")
            .HasConversion<ResourceTypeVO.ResourceTypeValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(typeof(ReviewStatusVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint unsigned")
            .HasConversion<ReviewStatusVO.ReviewStatusValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}