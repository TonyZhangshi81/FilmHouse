using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ResourceConfiguration : IEntityTypeConfiguration<ResourceEntity>
{
    public void Configure(EntityTypeBuilder<ResourceEntity> builder)
    {
        builder.HasKey(e => new { e.ResourceId });
        builder.HasAnnotation("SqlServer:Name", "resource_ix00");

        builder.ToTable("Resource");

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
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.FavorCount)
            .HasDefaultValue(typeof(FavorCountVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(4)")
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

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();


        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Resources)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Resource_UserAccount");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Resources)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Resource_Movie");

    }
}