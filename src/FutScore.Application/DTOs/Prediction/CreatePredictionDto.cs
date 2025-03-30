using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.Prediction
{
    public class CreatePredictionDto
    {
        [Required]
        public Guid MatchId { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Skor tahmini 'X-X' formatında olmalıdır")]
        [RegularExpression(@"^\d+-\d+$", ErrorMessage = "Skor tahmini 'X-X' formatında olmalıdır")]
        public string PredictedScore { get; set; }

        [Required]
        [StringLength(1)]
        [RegularExpression(@"^[1X2]$", ErrorMessage = "Sonuç tahmini 1, X veya 2 olmalıdır")]
        public string PredictedResult { get; set; }
    }
} 