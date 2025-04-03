namespace FutScore.Application.DTOs.League
{
    public class LeagueDto: BaseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required string LogoUrl { get; set; }
        public required int TeamCount { get; set; }
        public required int Level { get; set; }
    }
} 