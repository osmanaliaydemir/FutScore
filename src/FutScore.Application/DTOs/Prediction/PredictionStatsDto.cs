namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionStatsDto
    {
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public int WrongPredictions { get; set; }
        public double SuccessRate { get; set; }
        public int TotalPoints { get; set; }
        public double AveragePoints { get; set; }
        public int MaxPoints { get; set; }
        public int MinPoints { get; set; }
        public int MostPredictedResult { get; set; }
        public int LeastPredictedResult { get; set; }
    }
} 