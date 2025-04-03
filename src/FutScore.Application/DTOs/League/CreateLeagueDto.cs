namespace FutScore.Application.DTOs.League
{
    public class CreateLeagueDto
    {
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string LogoUrl { get; set; }
        public required int TeamCount { get; set; }
    }
} 