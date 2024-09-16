using System.Net.Mail;
using System.Net;

namespace ITIApp
{
    public class MailHelper
    {
        private string to {  get; set; }
        private string subject { get; set; }
        private string body {  get; set; }
        public MailHelper(string to, string subject, string body) {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
        public void Send()
        {
            try
            {

                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential
                        ("84320aca1b6827", "643ffb439e74f8"),
                    EnableSsl = true
                };
                client.Send("mailtrap@example.com", to, subject, body);

                Console.WriteLine($"Email Sent To {to} Subject {subject} body {body}");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message,ex.StackTrace);
            }
        }
    }
}
