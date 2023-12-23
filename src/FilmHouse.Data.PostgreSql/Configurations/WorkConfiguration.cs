using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmHouse.Data.Entities;
using FilmHouse.Core.ValueObjects;
using FilmHouse.Core.Utils.Data;

namespace FilmHouse.Data.PostgreSql.Configurations;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
internal class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    public void Configure(EntityTypeBuilder<WorkEntity> builder)
    {
        builder.ToTable("Work");

        builder.HasKey(e => new { e.WorkId }).HasName("work_ix00");

        builder.HasOne(d => d.Celebrity)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.CelebrityId)
            .HasConstraintName("FK_Work_Celebrity");

        builder.HasOne(d => d.Movie)
            .WithMany(p => p.Works)
            .HasForeignKey(d => d.MovieId)
            .HasConstraintName("FK_Work_Movie");


        builder.Property(e => e.RequestId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<RequestIdVO.RequestIdValueConverter>();

        builder.Property(e => e.WorkId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<WorkIdVO.WorkIdVOValueConverter>();

        builder.Property(e => e.MovieId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<MovieIdVO.MovieIdValueConverter>();

        builder.Property(e => e.CelebrityId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasConversion<CelebrityIdVO.CelebrityIdValueConverter>();

        builder.Property(e => e.Type)
            .HasDefaultValue(typeof(WorkTypeVO).CreateValueObjectInstance("0"))
            .HasColumnType("smallint")
            .HasConversion<WorkTypeVO.WorkTypeVOValueConverter>();

        builder.Property(e => e.CreatedOn)
            .IsRequired()
            .HasColumnType("timestamp(3)")
            .HasConversion<CreatedOnVO.CreatedOnValueConverter>();

        builder.Property(e => e.UpDatedOn)
            .HasColumnType("timestamp(3)")
            .HasConversion<UpDatedOnVO.UpDatedOnValueConverter>();

    }
}