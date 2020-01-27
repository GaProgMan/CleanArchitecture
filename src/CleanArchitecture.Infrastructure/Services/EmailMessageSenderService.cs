using System.Net.Mail;
using System.Threading.Tasks;
using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.Infrastructure.Services
{
    public class EmailMessageSenderService : IMessageSender
    {
        public void SendGuestbookNotificationEmail(string toAddress, string messageBody)
        {
            // message could be generated using a factory
            var message = new MailMessage();
            message.To.Add(toAddress);
            message.From = new MailAddress("donotreply@ddd-session.london");
            message.Subject = "New guestbook entry added";
            message.Body = messageBody;
            
            using var client = new SmtpClient("localhost", 25);
            client.Send(message);
        }
    }
}