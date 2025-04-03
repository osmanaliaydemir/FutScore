using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FutScore.Domain.ValueObjects;
using FutScore.Domain.Events.MatchEvents;
using FutScore.Domain.Events;

namespace FutScore.Domain.Entities
{
    public enum MatchStatus
    {
        Scheduled,
        Live,
        Completed,
        Postponed,
        Cancelled
    }

    public class Match : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Required]
        public int SeasonId { get; private set; }

        [Required]
        public int HomeTeamId { get; private set; }

        [Required]
        public int AwayTeamId { get; private set; }

        [Required]
        public int StadiumId { get; private set; }

        public Score Score { get; private set; }
        public MatchTime MatchTime { get; private set; }

        [Required]
        public MatchStatus Status { get; private set; }

        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        // Navigation Properties
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Season { get; private set; }

        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; private set; }

        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; private set; }

        [ForeignKey(nameof(StadiumId))]
        public virtual Stadium Stadium { get; private set; }

        protected Match() { }

        public Match(int seasonId, int homeTeamId, int awayTeamId, int stadiumId, DateTime matchDate)
        {
            SeasonId = seasonId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            StadiumId = stadiumId;
            MatchTime = new MatchTime(matchDate);
            Status = MatchStatus.Scheduled;
            Score = new Score(0, 0);
        }

        public void StartMatch()
        {
            if (Status != MatchStatus.Scheduled)
                throw new InvalidOperationException("Match can only be started when it is scheduled");

            Status = MatchStatus.Live;
            MatchTime = new MatchTime(MatchTime.MatchDate, DateTime.UtcNow);
            _domainEvents.Add(new MatchStartedEvent(this));
        }

        public void UpdateScore(int homeScore, int awayScore)
        {
            if (Status != MatchStatus.Live)
                throw new InvalidOperationException("Score can only be updated when match is live");

            Score = new Score(homeScore, awayScore);
        }

        public void EndMatch()
        {
            if (Status != MatchStatus.Live)
                throw new InvalidOperationException("Match can only be ended when it is live");

            Status = MatchStatus.Completed;
            MatchTime = new MatchTime(MatchTime.MatchDate, MatchTime.StartTime, DateTime.UtcNow);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
} 