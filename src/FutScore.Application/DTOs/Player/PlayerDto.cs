using FutScore.Application.DTOs.Team;

namespace FutScore.Application.DTOs.Player
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string FullName { get; set; }
        public string? Position { get; set; }

        public int? JerseyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public virtual TeamDto Team { get; set; }
    }
}
