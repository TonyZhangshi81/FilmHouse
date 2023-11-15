using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CommentConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(e => new { e.CommentId }).HasName("comment_ix00");

        builder.ToTable("Comment");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.CommentId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Content)
            .HasColumnType("longtext");

        builder.Property(e => e.CommentTime)
            .HasColumnType("datetime(3)");

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime(3)");

    }
}