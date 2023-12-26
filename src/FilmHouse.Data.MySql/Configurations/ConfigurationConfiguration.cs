using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ConfigurationConfiguration : IEntityTypeConfiguration<ConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<ConfigurationEntity> builder)
    {
        builder.ToTable("Configuration");

        builder.HasKey(e => new { e.Key }).HasName("configuration_ix00");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.Key)
            .HasColumnType("varchar(50)")
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion<ConfigKeyVO.ConfigKeyValueConverter>();

        builder.Property(e => e.Value)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<ConfigValueVO.ConfigValueValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("tinyint")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();
    }
}