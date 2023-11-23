using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Data.Core.ValueObjects;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class MovieConfiguration : IEntityTypeConfiguration<MovieEntity>
{
    public void Configure(EntityTypeBuilder<MovieEntity> builder)
    {
        builder.HasKey(e => new { e.MovieId });
        builder.HasAnnotation("SqlServer:Name", "movie_ix00");

        builder.ToTable("Movie");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.TitleEn)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Aka)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Directors)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Writers)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Casts)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.DirectorsId)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.WritersId)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.CastsId)
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Year)
            .HasColumnType("char(4)")
            .HasMaxLength(4);

        builder.Property(e => e.Pubdates)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(e => e.Durations)
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);

        builder.Property(e => e.Genres)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.Languages)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

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
            .HasColumnType("varchar(max)");

        builder.Property(e => e.Avatar)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50);

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.ReviewStatus)
            .HasDefaultValue(0)
            .HasColumnType("tinyint");

        builder.Property(e => e.ReviewNote)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100);

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<SysDateTimeVO.SysDateTimeValueConverter>();

        builder.Property(e => e.VisitCount)
            .HasDefaultValue(0)
            .HasColumnType("bigint");

    }
}