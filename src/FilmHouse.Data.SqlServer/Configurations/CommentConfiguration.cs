using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(e => new { e.CommentId });
        builder.HasAnnotation("SqlServer:Name", "comment_ix00");

        builder.ToTable("Comment");

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

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();



        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Comment_UserAccount");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Comments)
            .HasForeignKey(d => d.MovieId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Comment_Movie");

    }
}