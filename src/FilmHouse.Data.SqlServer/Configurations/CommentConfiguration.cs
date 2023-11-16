using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

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
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.CommentId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Content)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.CommentTime)
            .HasColumnType("datetime");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime");

    }
}