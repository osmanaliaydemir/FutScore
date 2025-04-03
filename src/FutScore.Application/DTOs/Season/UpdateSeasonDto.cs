namespace FutScore.Application.DTOs.Season
{
    public class UpdateSeasonDto
    {
        public int Id { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 