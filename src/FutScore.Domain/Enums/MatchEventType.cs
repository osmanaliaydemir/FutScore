namespace FutScore.Domain.Enums
{
    public enum MatchEventType
    {
        Goal = 1,
        OwnGoal = 2,
        PenaltyGoal = 3,
        YellowCard = 4,
        RedCard = 5,
        Substitution = 6,
        Injury = 7,
        VARDecision = 8,
        MatchStart = 9,
        HalfTime = 10,
        SecondHalfStart = 11,
        FullTime = 12,
        ExtraTimeStart = 13,
        ExtraTimeHalfTime = 14,
        ExtraTimeSecondHalfStart = 15,
        ExtraTimeEnd = 16,
        PenaltyShootoutStart = 17,
        PenaltyShootoutEnd = 18,
        MatchCancelled = 19,
        MatchPostponed = 20,
        MatchAbandoned = 21
    }
} 