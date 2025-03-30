namespace FutScore.Application.DTOs.League
{
    public class LeagueStatisticsDto
    {
        public int TotalMatches { get; set; }
        public int CompletedMatches { get; set; }
        public int UpcomingMatches { get; set; }
        public int TotalGoals { get; set; }
        public double AverageGoalsPerMatch { get; set; }
        public int TotalYellowCards { get; set; }
        public int TotalRedCards { get; set; }
        public int TotalPenalties { get; set; }
        public int MostGoalsScored { get; set; }
        public int MostGoalsConceded { get; set; }
        public string TopScorerName { get; set; }
        public int TopScorerGoals { get; set; }
        public string TopAssisterName { get; set; }
        public int TopAssisterAssists { get; set; }
        public string MostCleanSheetsTeam { get; set; }
        public int MostCleanSheets { get; set; }
        public string MostWinsTeam { get; set; }
        public int MostWins { get; set; }
        public string MostLossesTeam { get; set; }
        public int MostLosses { get; set; }
        public string MostDrawsTeam { get; set; }
        public int MostDraws { get; set; }
    }
} 