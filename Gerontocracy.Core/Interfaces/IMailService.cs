using System.Threading.Tasks;

using Gerontocracy.Core.BusinessObjects.Mail;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Gerontocracy.Core.Interfaces
{
    public interface IMailService
    {
        Task<Response> SendMailAsync(SendGridMessage message);
        Task SendConfirmationTokenAsync(MailConfirmationData data);
    }
}
