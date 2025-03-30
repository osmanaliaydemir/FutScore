namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionLeaderboardDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int TotalPoints { get; set; }
        public int CorrectPredictions { get; set; }
        public int TotalPredictions { get; set; }
        public double SuccessRate { get; set; }
    }
} 