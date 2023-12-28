using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.ToTable("Album");

        builder.HasKey(e => new { e.AlbumId }).HasName("album_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Albums)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Album_UserAccount");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AlbumId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<AlbumIdVO.AlbumIdValueConverter>();

        builder.Property(e => e.Title)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<AlbumTitleVO.AlbumTitleValueConverter>();

        builder.Property(e => e.UserId)
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Items)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("longtext")
            .HasConversion<AlbumJsonItemsVO.AlbumJsonItemsValueConverter>();

        builder.Property(e => e.Summary)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("longtext")
            .HasConversion<SummaryVO.SummaryValueConverter>();

        builder.Property(e => e.Cover)
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsUnicode(false)
            .HasConversion<CoverVO.CoverValueConverter>();

        builder.Property(e => e.AmountAttention)
            .HasDefaultValue(typeof(AmountAttentionVO).CreateValueObjectInstance("0"))
            .HasColumnType("numeric(8, 0)")
            .HasConversion<AmountAttentionVO.AmountAttentionValueConverter>();

        builder.Property(e => e.IsEnabled)
            .HasDefaultValue(typeof(IsEnabledVO).CreateValueObjectInstance("true"))
            .HasColumnType("tinyint")
            .HasConversion<IsEnabledVO.IsEnabledVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();




    }
}