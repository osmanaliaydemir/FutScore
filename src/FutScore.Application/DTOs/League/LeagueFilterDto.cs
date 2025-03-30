namespace FutScore.Application.DTOs.League
{
    public class LeagueFilterDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Season { get; set; }
        public bool? IsActive { get; set; }
        public string Status { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? EndDateFrom { get; set; }
        public DateTime? EndDateTo { get; set; }
        public int? MinTeams { get; set; }
        public int? MaxTeams { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
} 