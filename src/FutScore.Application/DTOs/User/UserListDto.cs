

namespace FutScore.Application.DTOs.User
{
    public class UserListDto 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public double PredictionAccuracy { get; set; }
        public int TotalPoints { get; set; }
        public int Rank { get; set; }
    }
} 