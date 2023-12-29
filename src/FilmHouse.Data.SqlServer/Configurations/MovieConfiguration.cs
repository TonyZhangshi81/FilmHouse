using FilmHouse.Core.Utils;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.ToTable("Movie");

        builder.HasKey(e => new { e.MovieId });
        builder.HasAnnotation("SqlServer:Name", "movie_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Movies)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Movie_UserAccount");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.Title)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<MovieTitleVO.MovieTitleValueConverter>();

        builder.Property(e => e.TitleEn)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<MovieTitleEnVO.MovieTitleEnValueConverter>();

        builder.Property(e => e.Aka)
            .HasColumnType("varchar(300)")
            .HasMaxLength(300)
            .HasConversion<MovieAkaVO.MovieAkaValueConverter>();

        builder.Property(e => e.Directors)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<DirectorNamesVO.DirectorNamesValueConverter>();

        builder.Property(e => e.Writers)
            .HasColumnType("varchar(500)")
            .HasMaxLength(500)
            .HasConversion<WritersNamesVO.WritersNamesValueConverter>();

        builder.Property(e => e.Casts)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<CastsNamesVO.CastsNamesValueConverter>();

        builder.Property(e => e.DirectorsId)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<DirectorsIdVO.DirectorsIdValueConverter>();

        builder.Property(e => e.WritersId)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<WritersIdVO.WritersIdValueConverter>();

        builder.Property(e => e.CastsId)
            .HasColumnType("varchar(1500)")
            .HasMaxLength(1500)
            .HasConversion<CastsIdVO.CastsIdValueConverter>();

        builder.Property(e => e.Year)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10)
            .HasConversion<YearVO.YearValueConverter>();

        builder.Property(e => e.Pubdates)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200)
            .HasConversion<PubdatesVO.PubdatesValueConverter>();

        builder.Property(e => e.Durations)
            .HasColumnType("varchar(10)")
            .HasMaxLength(10)
            .HasConversion<DurationsVO.DurationsValueConverter>();

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
            .HasColumnType("varchar(400)")
            .HasMaxLength(400)
            .HasConversion<CountriesVO.CountriesValueConverter>();

        builder.Property(e => e.Rating)
            .HasDefaultValue(typeof(RatingVO).CreateValueObjectInstance("0.0"))
            .HasColumnType("numeric(3, 1)")
            .HasMaxLength(4)
            .HasConversion<RatingVO.RatingValueConverter>();

        builder.Property(e => e.RatingCount)
            .HasDefaultValue(typeof(RatingCountVO).CreateValueObjectInstance("0"))
            .HasColumnType("int")
            .HasConversion<RatingCountVO.RatingCountValueConverter>();

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
            .HasColumnType("varchar(max)")
            .HasConversion<SummaryVO.SummaryValueConverter>();

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .HasConversion<MovieAvatarVO.MovieAvatarValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(typeof(ReviewStatusVO).CreateValueObjectInstance("0"))
            .HasColumnType("int")
            .HasConversion<ReviewStatusVO.ReviewStatusValueConverter>();

        builder.Property(e => e.Note)
            .HasColumnType("varchar(1000)")
            .HasMaxLength(1000)
            .HasConversion<NoteVO.NoteValueConverter>();

        builder.Property(e => e.PageViews)
            .HasDefaultValue(typeof(PageViewsVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(11, 0)")
            .HasMaxLength(11)
            .HasConversion<PageViewsVO.PageViewsValueConverter>();

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