using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KruglikLab
{
    public class StoredProcedures
    {

        public static void SendEmailUsingCLR(string receiver)//параметр это почта получателя
        {
            string _sender = "lilkrug2003@gmail.com";
            string _password = "tvwsbffejjpdgkyb";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                 new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new MailMessage(_sender, receiver);
                mail.Subject = "Voila..!! This email has been send using CLR Assembly.";
                mail.Body = "Voila..!! This email has been send using CLR Assembly.";
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
