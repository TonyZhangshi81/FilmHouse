﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class ResourceEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid ResourceId { get; set; }

    [Column(Order = 3)]
    public string Name { get; set; }

    [Column(Order = 4)]
    [Required]
    public string Content { get; set; }

    [Column(Order = 5)]
    public Int64 Size { get; set; }

    [Column(Order = 6)]
    public Guid UserId { get; set; }

    [Column(Order = 7)]
    public Guid MovieId { get; set; }

    [Column(Order = 8)]
    public DateTime Time { get; set; }

    [Column(Order = 9)]
    public int FavorCount { get; set; }

    [Column(Order = 10)]
    public Int16 Type { get; set; }

    [Column(Order = 11)]
    public Int16 ReviewStatus { get; set; }

    [Column(Order = 12)]
    public string ReviewNote { get; set; }

}