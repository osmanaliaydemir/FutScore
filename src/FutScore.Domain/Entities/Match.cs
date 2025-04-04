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
            if (seasonId <= 0)
                throw new ArgumentException("SeasonId must be greater than 0", nameof(seasonId));
            
            if (homeTeamId <= 0)
                throw new ArgumentException("HomeTeamId must be greater than 0", nameof(homeTeamId));
            
            if (awayTeamId <= 0)
                throw new ArgumentException("AwayTeamId must be greater than 0", nameof(awayTeamId));
            
            if (stadiumId <= 0)
                throw new ArgumentException("StadiumId must be greater than 0", nameof(stadiumId));
            
            if (homeTeamId == awayTeamId)
                throw new ArgumentException("Home team and away team cannot be the same", nameof(awayTeamId));
            
            if (matchDate < DateTime.UtcNow)
                throw new ArgumentException("Match date cannot be in the past", nameof(matchDate));

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

        public void UpdateStatus(MatchStatus newStatus)
        {
            if (Status == MatchStatus.Completed && newStatus != MatchStatus.Completed)
            {
                throw new InvalidOperationException("Completed match status cannot be changed");
            }

            if (Status == MatchStatus.Cancelled && newStatus != MatchStatus.Cancelled)
            {
                throw new InvalidOperationException("Cancelled match status cannot be changed");
            }

            Status = newStatus;
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
} 