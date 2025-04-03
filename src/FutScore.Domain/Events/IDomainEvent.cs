using System;

namespace FutScore.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
} 