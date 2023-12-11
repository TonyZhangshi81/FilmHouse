using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class AlbumConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(e => new { e.AlbumId }).HasName("album_ix00");

        builder.ToTable("Album");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.AlbumId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<AlbumIdVO.AlbumIdValueConverter>();

        builder.Property(e => e.Title)
            .HasColumnType("varchar(50)")
            .HasMaxLength(50)
            .HasConversion<AlbumTitleVO.AlbumTitleValueConverter>();

        builder.Property(e => e.UserId)
            .HasColumnType("uuid")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Items)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("text")
            .HasConversion<AlbumJsonItemsVO.AlbumJsonItemsValueConverter>();

        builder.Property(e => e.Summary)
            .HasComment("Variable-length character data, ⇐ 2G")
            .HasColumnType("text")
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
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();


        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Albums)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Album_UserAccount");


    }
}