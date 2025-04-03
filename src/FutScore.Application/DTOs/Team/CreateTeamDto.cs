namespace FutScore.Application.DTOs.Team
{
    public class CreateTeamDto
    {
        public required string Name { get; set; }
        public required int LeagueId { get; set; }
        public required int StadiumId { get; set; }
        public string? LogoUrl { get; set; }
        public required string City { get; set; }
    }
} 