using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class DiscoveryConfiguration : IEntityTypeConfiguration<DiscoveryEntity>
{
    public void Configure(EntityTypeBuilder<DiscoveryEntity> builder)
    {
        builder.ToTable("Discovery");

        builder.HasKey(e => new { e.DiscoveryId }).HasName("discovery_ix00");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Discoveries)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Discovery_Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.DiscoveryId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<DiscoveryIdVO.DiscoveryIdVOValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.Avatar)
            .IsRequired()
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<DiscoveryAvatarVO.DiscoveryAvatarVOValueConverter>();

        builder.Property(e => e.Order)
            .IsRequired()
            .HasColumnType("numeric(3, 0)")
            .HasConversion<SortOrderVO.SortOrderValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("boolean")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}