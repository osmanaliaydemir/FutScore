namespace FutScore.Domain.Enums
{
    public enum PermissionType
    {
        // User Permissions
        ViewProfile = 1,
        EditProfile = 2,
        DeleteProfile = 3,
        ChangePassword = 4,
        ManageRoles = 5,
        ManagePermissions = 6,

        // Match Permissions
        ViewMatches = 7,
        CreateMatch = 8,
        EditMatch = 9,
        DeleteMatch = 10,
        ManageMatchEvents = 11,
        ManageMatchPredictions = 12,

        // League Permissions
        ViewLeagues = 13,
        CreateLeague = 14,
        EditLeague = 15,
        DeleteLeague = 16,
        ManageLeagueTeams = 17,
        ManageLeagueMembers = 18,

        // Team Permissions
        ViewTeams = 19,
        CreateTeam = 20,
        EditTeam = 21,
        DeleteTeam = 22,
        ManageTeamPlayers = 23,
        ManageTeamStaff = 24,

        // Player Permissions
        ViewPlayers = 25,
        CreatePlayer = 26,
        EditPlayer = 27,
        DeletePlayer = 28,
        ManagePlayerStats = 29,
        ManagePlayerTransfers = 30,

        // Prediction Permissions
        ViewPredictions = 31,
        CreatePrediction = 32,
        EditPrediction = 33,
        DeletePrediction = 34,
        ManagePredictionResults = 35,
        ManagePredictionPoints = 36,

        // Friendship Permissions
        ViewFriends = 37,
        SendFriendRequest = 38,
        AcceptFriendRequest = 39,
        RejectFriendRequest = 40,
        RemoveFriend = 41,
        BlockUser = 42,

        // Notification Permissions
        ViewNotifications = 43,
        CreateNotification = 44,
        EditNotification = 45,
        DeleteNotification = 46,
        ManageNotificationTemplates = 47,
        SendNotification = 48,

        // System Permissions
        ViewSystemLogs = 49,
        ManageSystemSettings = 50,
        ManageUserSettings = 51,
        ManageEmailTemplates = 52,
        ManageSMS = 53,
        ManagePushNotifications = 54,

        // Analytics Permissions
        ViewAnalytics = 55,
        ExportAnalytics = 56,
        ManageAnalyticsSettings = 57,
        ViewUserAnalytics = 58,
        ViewMatchAnalytics = 59,
        ViewLeagueAnalytics = 60,

        // Payment Permissions
        ViewPayments = 61,
        ProcessPayments = 62,
        ManageSubscriptions = 63,
        ManageRefunds = 64,
        ViewPaymentReports = 65,
        ManagePaymentSettings = 66,

        // Content Permissions
        ViewContent = 67,
        CreateContent = 68,
        EditContent = 69,
        DeleteContent = 70,
        ManageContentCategories = 71,
        ManageContentTags = 72,

        // API Permissions
        UseAPI = 73,
        ManageAPIKeys = 74,
        ViewAPILogs = 75,
        ManageAPIRateLimits = 76,
        ManageAPIVersions = 77,
        ManageAPIDocumentation = 78
    }
} 