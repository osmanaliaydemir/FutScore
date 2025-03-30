using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Salt { get; set; }

        public bool IsEmailVerified { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiresAt { get; set; }

        public bool IsPhoneVerified { get; set; }
        public DateTime? PhoneVerifiedAt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }
        public string CoverPictureUrl { get; set; }

        public DateTime? LastLoginAt { get; set; }
        public string LastLoginIp { get; set; }
        public string LastLoginUserAgent { get; set; }

        public bool IsActive { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public string BlockReason { get; set; }

        // Navigation Properties
        public virtual UserProfile Profile { get; set; }
        public virtual UserSettings Settings { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        public virtual ICollection<FavoriteTeam> FavoriteTeams { get; set; }
        public virtual ICollection<Prediction> Predictions { get; set; }
        public virtual ICollection<UserStatistics> Statistics { get; set; }
        public virtual ICollection<UserNotification> Notifications { get; set; }
        public virtual ICollection<Friendship> SentFriendRequests { get; set; }
        public virtual ICollection<Friendship> ReceivedFriendRequests { get; set; }
        public virtual ICollection<UserBlock> BlockedUsers { get; set; }
        public virtual ICollection<UserBlock> BlockedByUsers { get; set; }
        public virtual ICollection<UserActivity> Activities { get; set; }
        public virtual ICollection<UserSession> Sessions { get; set; }
        public virtual ICollection<UserVerification> Verifications { get; set; }
        public virtual ICollection<UserSubscription> Subscriptions { get; set; }
        public virtual ICollection<UserPayment> Payments { get; set; }
        public virtual ICollection<UserReward> Rewards { get; set; }
        public virtual ICollection<UserChallenge> Challenges { get; set; }
        public virtual ICollection<UserLeaderboard> Leaderboards { get; set; }
        public virtual ICollection<UserBadge> Badges { get; set; }
        public virtual ICollection<UserFeedback> Feedbacks { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
        public virtual ICollection<UserSettings> UserSettings { get; set; }
        public virtual ICollection<UserPreference> UserPreferences { get; set; }
    }
} 