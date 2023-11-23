using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.MySql.Configurations;

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
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.MarkId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("smallint");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Target)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36);

        builder.Property(e => e.Time)
            .HasColumnType("datetime(3)");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

    }
}