using FutScore.Domain.Entities;

namespace FutScore.Domain.Events.MatchEvents
{
    public class MatchStartedEvent : DomainEvent
    {
        public int MatchId { get; }
        public int HomeTeamId { get; }
        public int AwayTeamId { get; }
        public int StadiumId { get; }

        public MatchStartedEvent(Match match)
        {
            MatchId = match.Id;
            HomeTeamId = match.HomeTeamId;
            AwayTeamId = match.AwayTeamId;
            StadiumId = match.StadiumId;
        }
    }
} 