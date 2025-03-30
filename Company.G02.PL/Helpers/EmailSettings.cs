using System.Net;
using System.Net.Mail;

namespace Company.G02.PL.Helpers
{
    public static class EmailSettings
    {
        public static bool sendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("fady41487@gmail.com", "oovxbthtrvuhtxse");
                client.Send("fady41487@gmail.com", email.To, email.Subject, email.Body);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }

        }
    }
}
