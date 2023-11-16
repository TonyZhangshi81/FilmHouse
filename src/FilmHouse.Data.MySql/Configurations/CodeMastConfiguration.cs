using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CodeMastConfiguration : IEntityTypeConfiguration<CodeMastEntity>
{
    public void Configure(EntityTypeBuilder<CodeMastEntity> builder)
    {
        builder.HasKey(e => new { e.Type, e.CodeId }).HasName("type_codeid_ix00");

        builder.ToTable("CodeMast");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)");

        builder.Property(e => e.Type)
            .IsRequired()
            .HasColumnType("varchar(10)")
            .HasMaxLength(10);

        builder.Property(e => e.CodeId)
            .IsRequired()
            .HasColumnType("varchar(10)")
            .HasMaxLength(10);

        builder.Property(e => e.CodeValue)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime(3)");

    }
}