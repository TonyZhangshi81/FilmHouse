using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.ToTable("Resource");

        builder.HasKey(e => new { e.ResourceId });
        builder.HasAnnotation("SqlServer:Name", "resource_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Resources)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Resource_UserAccount");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Resources)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Resource_Movie");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<ResourceIdVO.ResourceIdValueConverter>();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<ResourceNameVO.ResourceNameValueConverter>();

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("varchar(2000)")
            .HasMaxLength(2000)
            .HasConversion<ResourceContentVO.ResourceContentValueConverter>();

        builder.Property(e => e.Size)
            .HasDefaultValue(typeof(ResourceSizeVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(11, 0)")
            .HasConversion<ResourceSizeVO.ResourceSizeValueConverter>();

        builder.Property(e => e.UserId)
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(typeof(FavorCountVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(4, 0)")
            .HasConversion<FavorCountVO.FavorCountValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(ResourceTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint")
            .HasConversion<ResourceTypeVO.ResourceTypeValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(typeof(ReviewStatusVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint")
            .HasConversion<ReviewStatusVO.ReviewStatusValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("bit")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}