using FilmHouse.Data.Core.Utils;
using FilmHouse.Data.Core.ValueObjects;
using FilmHouse.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(e => new { e.AlbumId });
        builder.HasAnnotation("SqlServer:Name", "album_ix00");

        builder.ToTable("Album");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AlbumId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<AlbumIdVO.AlbumIdValueConverter>();

        builder.Property(e => e.Title)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<AlbumTitleVO.AlbumTitleValueConverter>();

        builder.Property(e => e.UserId)
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Items)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("varchar(max)")
            .HasConversion<AlbumJsonItemsVO.AlbumJsonItemsValueConverter>();

        builder.Property(e => e.Summary)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("varchar(max)")
            .HasConversion<SummaryVO.SummaryValueConverter>();

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasConversion<CoverVO.CoverValueConverter>();

        builder.Property(e => e.AmountAttention)
            .HasDefaultValue(typeof(AmountAttentionVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(4)")
            .HasConversion<AmountAttentionVO.AmountAttentionValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();
    }
}