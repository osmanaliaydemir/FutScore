namespace FutScore.Application.DTOs.Team
{
    public class TeamStatsDto
    {
        public int TotalMatches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public double WinPercentage { get; set; }
        public int CleanSheets { get; set; }
        public int FailedToScore { get; set; }
        public double AverageGoalsFor { get; set; }
        public double AverageGoalsAgainst { get; set; }
        public int BiggestWin { get; set; }
        public int BiggestLoss { get; set; }
        public int CurrentStreak { get; set; }
        public string Form { get; set; }
    }
} 