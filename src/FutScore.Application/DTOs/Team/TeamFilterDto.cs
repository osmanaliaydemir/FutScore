namespace FutScore.Application.DTOs.Team
{
    public class TeamFilterDto
    {
        public Guid? LeagueId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? MinPoints { get; set; }
        public int? MaxPoints { get; set; }
        public int? MinPosition { get; set; }
        public int? MaxPosition { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
} 