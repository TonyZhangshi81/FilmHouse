using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.MySql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class NoticeConfiguration : IEntityTypeConfiguration<NoticeEntity>
{
    public void Configure(EntityTypeBuilder<NoticeEntity> builder)
    {
        builder.ToTable("Notice");

        builder.HasKey(e => new { e.NoticeId }).HasName("notice_ix00");

        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Notices)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("FK_Notice_UserAccount");

        builder.HasOne(d => d.Resource)
            .WithMany(p => p.Notices)
            .HasForeignKey(d => d.ResourceId)
            .HasConstraintName("FK_Notice_Resource");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.NoticeId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<NoticeIdVO.NoticeIdValueConverter>();

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("longtext")
            .HasConversion<ContentVO.ContentValueConverter>();

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<ResourceIdVO.ResourceIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("char(36)")
            .HasMaxLength(36)
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Flag)
            .HasDefaultValue(typeof(NoticeFlagVO).CreateValueObjectInstance("false"))
            .HasColumnType("bit")
            .HasConversion<NoticeFlagVO.NoticeFlagValueConverter>();

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