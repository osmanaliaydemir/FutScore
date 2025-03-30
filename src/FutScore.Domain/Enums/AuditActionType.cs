namespace FutScore.Domain.Enums
{
    public enum AuditActionType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
        View = 4,
        Login = 5,
        Logout = 6,
        PasswordChange = 7,
        PasswordReset = 8,
        EmailChange = 9,
        PhoneChange = 10,
        ProfileUpdate = 11,
        RoleChange = 12,
        PermissionChange = 13,
        SubscriptionChange = 14,
        PaymentProcess = 15,
        FileUpload = 16,
        FileDownload = 17,
        ApiCall = 18,
        SystemConfiguration = 19,
        UserBlock = 20,
        UserUnblock = 21,
        UserReport = 22,
        UserVerification = 23,
        NotificationSend = 24,
        EmailSend = 25,
        SMSSend = 26,
        PushNotificationSend = 27,
        CacheClear = 28,
        DatabaseBackup = 29,
        DatabaseRestore = 30,
        SystemRestart = 31,
        SystemShutdown = 32
    }
} 