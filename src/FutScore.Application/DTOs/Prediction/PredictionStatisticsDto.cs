namespace FutScore.Application.DTOs.Prediction
{   
    public class PredictionStatisticsDto
    {
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public int TotalPoints { get; set; }
        public double AveragePoints { get; set; }
    }
} 