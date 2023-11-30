using MailKit.Net.Smtp;
using MimeKit;

using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementation
{
    internal class EmailServices : IEmailServices
    {
        public async Task<bool> SendEmail(string email, string Message, string Subject)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    client.Authenticate("ahmeds141000@gmail.com", "rnbjvxxmldpevqve");
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", "ahmeds141000@gmail.com"));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = Subject;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}