﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AskConfiguration : IEntityTypeConfiguration<AskEntity>
{
    public void Configure(EntityTypeBuilder<AskEntity> builder)
    {
        builder.HasKey(e => new { e.AskId }).HasName("ask_ix00");

        builder.ToTable("Ask");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.RequestTime)
            .HasColumnType("datetime(3)");

        builder.Property(e => e.RequestWith)
            .HasColumnType("int");

        builder.Property(e => e.Note)
            .HasColumnType("longtext");

        builder.Property(e => e.Status)
            .HasDefaultValue(false)
            .HasColumnType("tinyint");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)");

    }
}