using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

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
            .HasColumnType("uuid")
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
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(typeof(FavorCountVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(11)")
            .HasConversion<FavorCountVO.FavorCountValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(ResourceTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("smallint")
            .HasConversion<ResourceTypeVO.ResourceTypeValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(typeof(ReviewStatusVO).CreateValueObjectInstance("0"))
            .HasColumnType("int")
            .HasConversion<ReviewStatusVO.ReviewStatusValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}