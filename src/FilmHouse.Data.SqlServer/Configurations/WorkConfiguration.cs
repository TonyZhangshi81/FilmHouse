﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    public void Configure(EntityTypeBuilder<WorkEntity> builder)
    {
        builder.ToTable("Work");

        builder.HasKey(e => new { e.WorkId });
        builder.HasAnnotation("SqlServer:Name", "work_ix00");

        builder.HasOne(d => d.Celebrity)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.CelebrityId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Work_Celebrity");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Work_Movie");



        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.WorkId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<WorkIdVO.WorkIdVOValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.CelebrityId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<CelebrityIdVO.CelebrityIdValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(WorkTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint")
            .HasConversion<WorkTypeVO.WorkTypeVOValueConverter>();

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