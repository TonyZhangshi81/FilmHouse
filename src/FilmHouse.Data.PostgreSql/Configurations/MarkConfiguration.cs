using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class MarkConfiguration : IEntityTypeConfiguration<MarkEntity>
{
    public void Configure(EntityTypeBuilder<MarkEntity> builder)
    {
        builder.HasKey(e => new { e.MarkId }).HasName("mark_ix00");

        builder.ToTable("Mark");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.MarkId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Type)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("smallint");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Target)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Time)
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

    }
}