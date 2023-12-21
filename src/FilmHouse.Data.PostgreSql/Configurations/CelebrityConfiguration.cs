using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class CelebrityConfiguration : IEntityTypeConfiguration<CelebrityEntity>
{
    public void Configure(EntityTypeBuilder<CelebrityEntity> builder)
    {
        builder.ToTable("Celebrity");

        builder.HasKey(e => new { e.CelebrityId }).HasName("celebrity_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Celebrities)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Celebrity_UserAccount");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.CelebrityId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<CelebrityIdVO.CelebrityIdValueConverter>();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .HasConversion<CelebrityNameVO.CelebrityNameValueConverter>();

        builder.Property(e => e.Aka)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<CelebrityAkaVO.CelebrityAkaValueConverter>();

        builder.Property(e => e.NameEn)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .HasConversion<CelebrityNameEnVO.CelebrityNameEnValueConverter>();

        builder.Property(e => e.AkaEn)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<CelebrityAkaEnVO.CelebrityAkaEnValueConverter>();

        builder.Property(e => e.Gender)
            .HasColumnType("smallint")
            .HasConversion<GenderVO.GenderValueConverter>();

        builder.Property(e => e.Professions)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<ProfessionsVO.ProfessionsValueConverter>();

        builder.Property(e => e.Birthday)
            .HasColumnType("date")
            .HasConversion<BirthdayVO.BirthdayValueConverter>();

        builder.Property(e => e.Deathday)
            .HasColumnType("date")
            .HasConversion<DeathdayVO.DeathdayValueConverter>();

        builder.Property(e => e.BornPlace)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<BornPlaceVO.BornPlaceValueConverter>();

        builder.Property(e => e.Family)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<FamiliesVO.FamilyValueConverter>();

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<StarAvatarVO.StarAvatarValueConverter>();

        builder.Property(e => e.WorksId)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<WorksVO.WorksValueConverter>();

        builder.Property(e => e.DoubanID)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10)
            .HasConversion<DoubanIDVO.DoubanIDValueConverter>();

        builder.Property(e => e.IMDb)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10)
            .HasConversion<IMDbVO.IMDbValueConverter>();

        builder.Property(e => e.Summary)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("text")
            .HasConversion<SummaryVO.SummaryValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(typeof(ReviewStatusVO).CreateValueObjectInstance("0"))
            .HasColumnType("smallint")
            .HasConversion<ReviewStatusVO.ReviewStatusValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}