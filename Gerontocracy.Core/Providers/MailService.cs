using System.Threading.Tasks;

using Gerontocracy.Core.BusinessObjects.Mail;
using Gerontocracy.Core.Config;
using Gerontocracy.Core.Interfaces;

using SendGrid;
using SendGrid.Helpers.Mail;


namespace Gerontocracy.Core.Providers
{
    public class MailService : IMailService
    {
        #region Fields

        private readonly GerontocracySettings _gerontocracySettings;

        private readonly ISendGridClient _sendGridClient;

        #endregion Fields

        #region Constructors

        public MailService(GerontocracySettings gerontocracySettings, ISendGridClient sendGridClient)
        {
            this._sendGridClient = sendGridClient;
            this._gerontocracySettings = gerontocracySettings;
        }

        #endregion Constructors

        #region Methods

        public async Task SendConfirmationTokenAsync(MailConfirmationData data)
        {
            var from = new EmailAddress(this._gerontocracySettings.MailAddress, this._gerontocracySettings.MailSender);
            var to = new EmailAddress(data.EmailAddress);
            var subject = "Willkommen bei " + _gerontocracySettings.AppName + "!";
            var link = this._gerontocracySettings.AppUri + "confirmemail/?userId=" + data.Id + "&token=" + data.Token;
            var bodyPlain = "Willkommen bei " + _gerontocracySettings.AppName + ", " + data.Name + "! Bitte kopiere folgende URL in die Browseradresszeile: " + link;
            var bodyHtml = "<p>Willkommen bei " + _gerontocracySettings.AppName + ", " + data.Name + "!</p>" +
                "<p>Bitte klicke auf folgenden Link zum Bestätigen deiner E-Mail-Adresse:" +
                "<br/><a href=\"" + link + "\">Hier klicken!</a></p>" +
                "<p>Falls der Link nicht angezeigt wird kopiere bitte folgende URL in die Browseradresszeile:<br/>" +
                link + "</p>";

            var message = MailHelper.CreateSingleEmail(from, to, subject, bodyPlain, bodyHtml);

            await this.SendMailAsync(message);
        }

        public Task<Response> SendMailAsync(SendGridMessage message) => _sendGridClient.SendEmailAsync(message);

        #endregion Methods
    }
}
