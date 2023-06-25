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

        public static void SendEmailUsingCLR()
        {
            string _sender = "lilkrug2003@gmail.com";
            string _password = "kolalalka";
            string _Receiver = "lilkrug2003@gmail.com";
            SmtpClient client = new SmtpClient("smtp.gmail.com");//провайдера вставь своего яндексы хуяндексы
            client.Port = 587;// порт тоже укажи свой там будет написан
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                 new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;
            try
            {
                var mail = new MailMessage(_sender, _Receiver);
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
