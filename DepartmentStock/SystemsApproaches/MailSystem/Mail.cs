using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace DepartmentStock.SystemsApproaches.MailSystem
{
    public static class SendMailVia
    {
        public static void SendViaGmail(string ReceiverEmail, string SenderEmail, string SenderEmailPassword, string MailBody, string MailSubject)
        {

            Imail i = new Email();
            i.SendMail(ReceiverEmail, SenderEmail, SenderEmailPassword, MailBody, MailSubject, "smtp.gmail.com", 587);
        }

    }

    public interface Imail
    {
        void SendMail(string ReceiverEmail, string SenderEmail, string SenderEmailPassword, string MailBody, string MailSubject, string Host, int Port);
    }


    public class Email : Imail
    {

        public void SendMail(string ReceiverEmail, string SenderEmail, string SenderEmailPassword, string MailBody,   string MailSubject, string Host, int Port)
        {
            string to = ReceiverEmail; //To address    
            string from = SenderEmail; //From address    
            MailMessage message = new MailMessage(from, to);
            //message.Attachments.Add(new Attachment(PathAttachments));
            string mailbody = MailBody;
            message.Subject = MailSubject;
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(Host, Port); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential(SenderEmail, SenderEmailPassword);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}