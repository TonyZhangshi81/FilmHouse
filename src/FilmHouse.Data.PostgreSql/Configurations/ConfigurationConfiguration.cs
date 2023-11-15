using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

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
            .HasColumnType("uuid");

        builder.Property(e => e.Key)
            .HasColumnType("varchar(64)")
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(e => e.Value)
            .HasColumnType("text");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)");

    }
}