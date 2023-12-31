﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.ToTable("Comment");

        builder.HasKey(e => new { e.CommentId });
        builder.HasAnnotation("SqlServer:Name", "comment_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Comment_UserAccount")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Comment_Movie")
            .OnDelete(DeleteBehavior.NoAction);


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.CommentId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<CommentIdVO.CommentIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.Content)
            .HasColumnType("varchar(max)")
            .HasConversion<ContentVO.ContentValueConverter>();

        builder.Property(e => e.CommentTime)
            .HasColumnType("datetime")
            .HasConversion<CommentTimeVO.CommentTimeValueConverter>();

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