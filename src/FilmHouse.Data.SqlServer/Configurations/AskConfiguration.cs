using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AskConfiguration : IEntityTypeConfiguration<AskEntity>
{
    public void Configure(EntityTypeBuilder<AskEntity> builder)
    {
        builder.HasKey(e => new { e.AskId });
        builder.HasAnnotation("SqlServer:Name", "ask_ix00");

        builder.ToTable("Ask");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.RequestTime)
            .HasColumnType("datetime");

        builder.Property(e => e.RequestWith)
            .HasColumnType("int");

        builder.Property(e => e.Note)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.State)
            .HasDefaultValue(false)
            .HasColumnType("bit");

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime");

    }
}