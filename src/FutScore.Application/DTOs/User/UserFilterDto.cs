namespace FutScore.Application.DTOs.User
{
    public class UserFilterDto
    {
        public string SearchTerm { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool? IsActive { get; set; }
        public string Role { get; set; }
        public DateTime? CreatedAtFrom { get; set; }
        public DateTime? CreatedAtTo { get; set; }
        public DateTime? LastLoginAtFrom { get; set; }
        public DateTime? LastLoginAtTo { get; set; }
        public int? MinPredictions { get; set; }
        public int? MaxPredictions { get; set; }
        public double? MinPredictionAccuracy { get; set; }
        public double? MaxPredictionAccuracy { get; set; }
        public int? MinPoints { get; set; }
        public int? MaxPoints { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
} 