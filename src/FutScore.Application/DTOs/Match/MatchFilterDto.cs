namespace FutScore.Application.DTOs.Match
{
    public class MatchFilterDto
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string LeagueName { get; set; }
        public string Season { get; set; }
        public string Status { get; set; }
        public DateTime? MatchDateFrom { get; set; }
        public DateTime? MatchDateTo { get; set; }
        public string Venue { get; set; }
        public string Referee { get; set; }
        public int? MinAttendance { get; set; }
        public int? MaxAttendance { get; set; }
        public int? MinGoals { get; set; }
        public int? MaxGoals { get; set; }
        public string Round { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
} 