﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmHouse.Data.Entities;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public class MovieEntity : EntitiesBase
{
    [Column(Order = 2)]
    [Required]
    public Guid MovieId { get; set; }

    [Column(Order = 3)]
    [Required]
    public string Title { get; set; }

    [Column(Order = 4)]
    public string TitleEn { get; set; }

    [Column(Order = 5)]
    public string Aka { get; set; }

    [Column(Order = 6)]
    public string Directors { get; set; }

    [Column(Order = 7)]
    public string Writers { get; set; }

    [Column(Order = 8)]
    public string Casts { get; set; }

    [Column(Order = 9)]
    public string DirectorsId { get; set; }

    [Column(Order = 10)]
    public string WritersId { get; set; }

    [Column(Order = 11)]
    public string CastsId { get; set; }

    [Column(Order = 12)]
    public string Year { get; set; }

    [Column(Order = 13)]
    public string Pubdates { get; set; }

    [Column(Order = 14)]
    public string Durations { get; set; }

    [Column(Order = 15)]
    public string Genres { get; set; }

    [Column(Order = 16)]
    public string Languages { get; set; }

    [Column(Order = 17)]
    public string Countries { get; set; }

    [Column(Order = 18)]
    public decimal Rating { get; set; }

    [Column(Order = 19)]
    public int RatingCount { get; set; }

    [Column(Order = 20)]
    public string DoubanID { get; set; }

    [Column(Order = 21)]
    public string IMDb { get; set; }

    [Column(Order = 22)]
    public string Summary { get; set; }

    [Column(Order = 23)]
    public string Avatar { get; set; }

    [Column(Order = 24)]
    [Required]
    public Guid UserId { get; set; }

    [Column(Order = 25)]
    public Int16 ReviewStatus { get; set; }

    [Column(Order = 26)]
    public string ReviewNote { get; set; }

    [Column(Order = 27)]
    public Int64 VisitCount { get; set; }

}