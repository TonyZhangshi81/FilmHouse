﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AskConfiguration : IEntityTypeConfiguration<AskEntity>
{
    public void Configure(EntityTypeBuilder<AskEntity> builder)
    {
        builder.ToTable("Ask");

        builder.HasKey(e => new { e.AskId }).HasName("ask_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Asks)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Ask_UserAccount");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Asks)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Ask_Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<AskIdVO.AskIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.RequestTime)
            .HasColumnType("timestamp(3)")
            .HasConversion<RequestTimeVO.RequestTimeValueConverter>();

        builder.Property(e => e.RequestWith)
            .HasDefaultValue(typeof(RequestWithVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(4)")
            .HasConversion<RequestWithVO.RequestWithValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.Status)
            .HasDefaultValue(typeof(AskStatusVO).CreateValueObjectInstance("false"))
            .HasColumnType("boolean")
            .HasConversion<AskStatusVO.AskStatusValueConverter>();

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