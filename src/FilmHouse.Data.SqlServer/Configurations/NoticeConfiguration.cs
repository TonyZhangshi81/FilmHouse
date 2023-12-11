using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.SqlServer.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class NoticeConfiguration : IEntityTypeConfiguration<NoticeEntity>
{
    public void Configure(EntityTypeBuilder<NoticeEntity> builder)
    {
        builder.HasKey(e => new { e.NoticeId });
        builder.HasAnnotation("SqlServer:Name", "notice_ix00");

        builder.ToTable("Notice");

        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.NoticeId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<NoticeIdVO.NoticeIdValueConverter>();

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("varchar(max)")
            .HasConversion<ContentVO.ContentValueConverter>();

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<ResourceIdVO.ResourceIdValueConverter>();

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier")
            .HasConversion<UserIdVO.UserIdValueConverter>();

        builder.Property(e => e.Flag)
            .HasDefaultValue(typeof(NoticeFlagVO).CreateValueObjectInstance("false"))
            .HasColumnType("bit")
            .HasConversion<NoticeFlagVO.NoticeFlagValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();


        builder.HasOne(d => d.UserAccount)
            .WithMany(p => p.Notices)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Notice_UserAccount");

        builder.HasOne(d => d.Resource)
            .WithMany(p => p.Notices)
            .HasForeignKey(d => d.ResourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Notice_Resource");

    }
}