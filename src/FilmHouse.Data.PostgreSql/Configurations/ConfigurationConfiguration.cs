using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class ConfigurationConfiguration : IEntityTypeConfiguration<ConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<ConfigurationEntity> builder)
    {
        builder.HasKey(e => new { e.Key }).HasName("configuration_ix00");

        builder.ToTable("Configuration");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
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
            .HasColumnType("boolean")
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