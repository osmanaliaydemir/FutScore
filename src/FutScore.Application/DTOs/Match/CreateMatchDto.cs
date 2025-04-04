using FutScore.Application.DTOs.Stadium;
using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.Match
{
    public class CreateMatchDto
    {
        [Required(ErrorMessage = "Sezon seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir sezon seçiniz.")]
        public int SeasonId { get; set; }

        [Required(ErrorMessage = "Ev sahibi takım seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir ev sahibi takım seçiniz.")]
        public int HomeTeamId { get; set; }

        [Required(ErrorMessage = "Deplasman takımı seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir deplasman takımı seçiniz.")]
        public int AwayTeamId { get; set; }

        [Required(ErrorMessage = "Maç tarihi zorunludur.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Maç tarihi geçmiş bir tarih olamaz.")]
        public DateTime MatchDate { get; set; }

        [Required(ErrorMessage = "Stadyum seçimi zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Geçerli bir stadyum seçiniz.")]
        public int StadiumId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Ev sahibi skoru 0'dan küçük olamaz.")]
        public int? HomeTeamScore { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Deplasman skoru 0'dan küçük olamaz.")]
        public int? AwayTeamScore { get; set; }

        [Required(ErrorMessage = "Maç durumu zorunludur.")]
        public string Status { get; set; }

        public ICollection<StadiumDto>? Stadium { get; set; }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date >= DateTime.UtcNow;
            }
            return false;
        }
    }
}