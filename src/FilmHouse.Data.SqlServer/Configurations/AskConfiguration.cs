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
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.AskId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.RequestTime)
            .HasColumnType("datetime");

        builder.Property(e => e.RequestWith)
            .HasColumnType("int");

        builder.Property(e => e.Note)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Status)
            .HasDefaultValue(false)
            .HasColumnType("bit");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime");

    }
}