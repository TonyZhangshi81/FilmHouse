using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ConfigurationConfiguration : IEntityTypeConfiguration<ConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<ConfigurationEntity> builder)
    {
        builder.HasKey(e => new { e.Key });
        builder.HasAnnotation("SqlServer:Name", "configuration_ix00");

        builder.ToTable("Configuration");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.Key)
            .HasColumnType("varchar(64)")
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.Value)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();
    }
}