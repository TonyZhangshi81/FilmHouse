﻿using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class DiscoveryConfiguration : IEntityTypeConfiguration<DiscoveryEntity>
{
    public void Configure(EntityTypeBuilder<DiscoveryEntity> builder)
    {
        builder.ToTable("Discovery");

        builder.HasKey(e => new { e.DiscoveryId });
        builder.HasAnnotation("SqlServer:Name", "discovery_ix00");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Discoveries)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Discovery_Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.DiscoveryId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<DiscoveryIdVO.DiscoveryIdVOValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
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