using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.PostgreSql.Configurations;

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
            .HasColumnType("text");

        builder.Property(e => e.CommentTime)
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)");

    }
}