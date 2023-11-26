using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasKey(e => new { e.MovieId }).HasName("movie_ix00");

        builder.ToTable("Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<MovieTitleVO.MovieTitleValueConverter>();

        builder.Property(e => e.TitleEn)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Aka)
            .HasColumnType("text");

        builder.Property(e => e.Directors)
            .HasColumnType("text");

        builder.Property(e => e.Writers)
            .HasColumnType("text");

        builder.Property(e => e.Casts)
            .HasColumnType("text");

        builder.Property(e => e.DirectorsId)
            .HasColumnType("text");

        builder.Property(e => e.WritersId)
            .HasColumnType("text");

        builder.Property(e => e.CastsId)
            .HasColumnType("text");

        builder.Property(e => e.Year)
            .HasColumnType("char(4)")
            .HasMaxLength(100);

        builder.Property(e => e.Pubdates)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(e => e.Durations)
            .HasColumnType("varchar(200)")
            .HasMaxLength(100);

        builder.Property(e => e.Genres)
            .HasColumnType("varchar(400)")
            .HasMaxLength(400)
            .IsUnicode(false)
            .HasConversion<GenresVO.GenresValueConverter>();

        builder.Property(e => e.Languages)
            .HasColumnType("varchar(400)")
            .HasMaxLength(400)
            .IsUnicode(false)
            .HasConversion<LanguagesVO.LanguagesValueConverter>();

        builder.Property(e => e.Countries)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Rating)
            .HasColumnType("numeric(3,1)");

        builder.Property(e => e.RatingCount)
            .HasColumnType("int");

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

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue("0")
            .HasColumnType("smallint");

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.PageViews)
            .HasDefaultValue(typeof(PageViewsVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(11)")
            .HasConversion<PageViewsVO.PageViewsValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();
    }
}