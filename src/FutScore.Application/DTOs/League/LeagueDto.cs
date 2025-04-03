namespace FutScore.Application.DTOs.League
{
    public class LeagueDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string LogoUrl { get; set; }
        public required string TeamCount { get; set; }
    }
} 