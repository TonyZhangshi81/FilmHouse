using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CodeMastConfiguration : IEntityTypeConfiguration<CodeMastEntity>
{
    public void Configure(EntityTypeBuilder<CodeMastEntity> builder)
    {
        builder.HasKey(e => new { e.Type, e.CodeId });
        builder.HasAnnotation("SqlServer:Name", "type_codeid_ix00");

        builder.ToTable("CodeMast");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.CodeId)
            .IsRequired()
            .HasColumnType("varchar(36)")
            .HasMaxLength(36);

        builder.Property(e => e.CodeValue)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UpDatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime");

    }
}