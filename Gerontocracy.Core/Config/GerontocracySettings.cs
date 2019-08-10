namespace Gerontocracy.Core.Config
{
    public class GerontocracySettings
    {
        public bool AutoConfirmEmails { get; set; }
        public bool AutoAdmin { get; set; }
        public string SendGridApiKey { get; set; }
        public string MailAddress { get; set; }
        public string MailSender { get; set; }
        public string UrlNationalrat { get; set; }
        public string UrlParteien { get; set; }
        public string AppName { get; set; }
        public string AppUri { get; set; }
        public bool SyncActive { get; set; }
    }
}
