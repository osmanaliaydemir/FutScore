namespace FutScore.Application.DTOs.League
{
    public class TeamStandingDto
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamLogoUrl { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }
        public string Form { get; set; } // Son 5 maçın sonuçları (W: Win, D: Draw, L: Loss)
    }
} 