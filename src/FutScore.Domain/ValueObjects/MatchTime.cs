using System;

namespace FutScore.Domain.ValueObjects
{
    public class MatchTime : ValueObject
    {
        public DateTime MatchDate { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }

        public MatchTime(DateTime matchDate, DateTime? startTime = null, DateTime? endTime = null)
        {
            if (startTime.HasValue && startTime.Value < matchDate)
                throw new ArgumentException("Start time cannot be before match date");

            if (endTime.HasValue && startTime.HasValue && endTime.Value < startTime.Value)
                throw new ArgumentException("End time cannot be before start time");

            MatchDate = matchDate;
            StartTime = startTime;
            EndTime = endTime;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MatchDate;
            yield return StartTime;
            yield return EndTime;
        }

        public bool IsScheduled => !StartTime.HasValue && !EndTime.HasValue;
        public bool IsInProgress => StartTime.HasValue && !EndTime.HasValue;
        public bool IsCompleted => StartTime.HasValue && EndTime.HasValue;
    }
} 