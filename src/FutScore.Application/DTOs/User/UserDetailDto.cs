using FutScore.Application.DTOs.Prediction;


namespace FutScore.Application.DTOs.User
{
    public class UserDetailDto 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastPasswordChangeAt { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public double PredictionAccuracy { get; set; }
        public int TotalPoints { get; set; }
        public int Rank { get; set; }
        public int Streak { get; set; }
        public int BestStreak { get; set; }
        public int WorstStreak { get; set; }
        public double AveragePointsPerPrediction { get; set; }
        public List<PredictionDto> RecentPredictions { get; set; }
        public List<PredictionDto> TopPredictions { get; set; }
        public List<PredictionDto> WorstPredictions { get; set; }
        public Dictionary<string, double> PredictionAccuracyByLeague { get; set; }
        public Dictionary<string, int> PointsByLeague { get; set; }
    }
} 