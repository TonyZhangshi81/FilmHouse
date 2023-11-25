using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CelebrityConfiguration : IEntityTypeConfiguration<CelebrityEntity>
{
    public void Configure(EntityTypeBuilder<CelebrityEntity> builder)
    {
        builder.HasKey(e => new { e.CelebrityId }).HasName("celebrity_ix00");

        builder.ToTable("Celebrity");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.CelebrityId)
            .IsRequired()
            .HasColumnType("uuid");

        builder.Property(e => e.Name)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Aka)
            .HasColumnType("text");

        builder.Property(e => e.NameEn)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.AkaEn)
            .HasColumnType("text");

        builder.Property(e => e.Gender)
            .HasColumnType("smallint");

        builder.Property(e => e.Occupation)
            .HasColumnType("text");

        builder.Property(e => e.Birthday)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10);

        builder.Property(e => e.Deathday)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10);

        builder.Property(e => e.BornPlace)
            .HasColumnType("varchar(100)")
            .HasMaxLength(10);

        builder.Property(e => e.Family)
            .HasColumnType("text");

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Works)
            .HasColumnType("text");

        builder.Property(e => e.DoubanID)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.IMDb)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.Summary)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("text")
            .HasConversion<SummaryVO.SummaryValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(0)
            .HasColumnType("smallint");

        builder.Property(e => e.ReviewNote)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}