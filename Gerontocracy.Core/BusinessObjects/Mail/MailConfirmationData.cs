namespace Gerontocracy.Core.BusinessObjects.Mail
{
    public class MailConfirmationData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
