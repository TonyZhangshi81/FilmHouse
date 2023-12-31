﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    public void Configure(EntityTypeBuilder<WorkEntity> builder)
    {
        builder.ToTable("Work");

        builder.HasKey(e => new { e.WorkId }).HasName("work_ix00");

        builder.HasOne(d => d.Celebrity)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.CelebrityId)
            .HasConstraintName("FK_Work_Celebrity");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Work_Movie");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.WorkId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<WorkIdVO.WorkIdVOValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.CelebrityId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<CelebrityIdVO.CelebrityIdValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(WorkTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("tinyint unsigned")
            .HasConversion<WorkTypeVO.WorkTypeVOValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("tinyint")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}