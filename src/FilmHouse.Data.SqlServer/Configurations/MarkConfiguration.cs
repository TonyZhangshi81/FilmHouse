using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class MarkConfiguration : IEntityTypeConfiguration<MarkEntity>
{
    public void Configure(EntityTypeBuilder<MarkEntity> builder)
    {
        builder.HasKey(e => new { e.MarkId });
        builder.HasAnnotation("SqlServer:Name", "mark_ix00");

        builder.ToTable("Mark");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.MarkId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Type)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("tinyint");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Target)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Time)
            .HasColumnType("datetime");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime");

    }
}