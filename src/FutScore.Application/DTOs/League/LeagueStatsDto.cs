namespace FutScore.Application.DTOs.League
{
    public class LeagueStatsDto
    {
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int CompletedMatches { get; set; }
        public int UpcomingMatches { get; set; }
        public int TotalGoals { get; set; }
        public double AverageGoalsPerMatch { get; set; }
        public int HighestScoringMatch { get; set; }
        public int LowestScoringMatch { get; set; }
        public int MostGoalsScoredByTeam { get; set; }
        public int MostGoalsConcededByTeam { get; set; }
        public double AverageAttendance { get; set; }
        public int HighestAttendance { get; set; }
        public int LowestAttendance { get; set; }
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
        public double AverageCardsPerMatch { get; set; }
        public int PenaltiesAwarded { get; set; }
        public int PenaltiesScored { get; set; }
        public double PenaltyConversionRate { get; set; }
        public int HomeWins { get; set; }
        public int AwayWins { get; set; }
        public int Draws { get; set; }
        public double HomeWinPercentage { get; set; }
        public double AwayWinPercentage { get; set; }
        public double DrawPercentage { get; set; }
    }
} 