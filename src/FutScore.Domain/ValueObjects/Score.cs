namespace FutScore.Domain.ValueObjects
{
    public class Score : ValueObject
    {
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }

        public Score(int homeScore, int awayScore)
        {
            HomeScore = homeScore;
            AwayScore = awayScore;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HomeScore;
            yield return AwayScore;
        }

        public bool IsDraw => HomeScore == AwayScore;
        public bool IsHomeWin => HomeScore > AwayScore;
        public bool IsAwayWin => HomeScore < AwayScore;
    }
} 