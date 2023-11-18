using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;

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
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.NoticeId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Content)
            .IsRequired()
            .HasColumnType("varchar(max)");

        builder.Property(e => e.ResourceId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.UserId)
            .IsRequired()
            .HasColumnType("uniqueidentifier");

        builder.Property(e => e.Time)
            .HasColumnType("datetime");

        builder.Property(e => e.Flag)
            .HasDefaultValue(0)
            .HasColumnType("tinyint");

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("datetime");

    }
}