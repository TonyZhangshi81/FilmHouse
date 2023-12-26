using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CodeMastConfiguration : IEntityTypeConfiguration<CodeMastEntity>
{
    public void Configure(EntityTypeBuilder<CodeMastEntity> builder)
    {
        builder.ToTable("CodeMast");

        builder.HasKey(e => new { e.Group, e.Code });
        builder.HasAnnotation("SqlServer:Name", "group_code_ix00");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
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
            .HasColumnType("bit")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}