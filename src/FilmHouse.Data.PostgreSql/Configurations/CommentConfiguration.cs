﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comment");

        builder.HasKey(e => new { e.CommentId }).HasName("comment_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Comment_UserAccount");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Comment_Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.CommentId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<CommentIdVO.CommentIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.Content)
            .HasColumnType("text")
            .HasConversion<ContentVO.ContentValueConverter>();

        builder.Property(e => e.CommentTime)
            .HasColumnType("timestamp(3)")
            .HasConversion<CommentTimeVO.CommentTimeValueConverter>();

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