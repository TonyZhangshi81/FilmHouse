using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

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
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

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

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

    }
}