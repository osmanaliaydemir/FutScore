namespace FutScore.Application.DTOs.Season
{
    public class CreateSeasonDto
    {
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
} 