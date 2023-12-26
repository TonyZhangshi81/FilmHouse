using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CodeMastConfiguration : IEntityTypeConfiguration<CodeMastEntity>
{
    public void Configure(EntityTypeBuilder<CodeMastEntity> builder)
    {
        builder.ToTable("CodeMast");

        builder.HasKey(e => new { e.Group, e.Code }).HasName("group_code_ix00");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.Group)
            .IsRequired()
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .HasConversion<CodeGroupVO.CodeGroupValueConverter>();

        builder.Property(e => e.Code)
            .IsRequired()
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .HasConversion<CodeKeyVO.CodeKeyValueConverter>();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<CodeValueVO.CodeValueConverter>();

        builder.Property(e => e.Order)
            .IsRequired()
            .HasColumnType("numeric(3)")
            .HasConversion<SortOrderVO.SortOrderValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("tinyint")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}