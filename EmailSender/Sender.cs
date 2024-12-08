using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public struct ConfigsEmail
    {
        public string Sender;
        public string To;
        public string Subject;
        public string Body;
        public string Server;
        public string Attachment;
        public int Port;
    }
    public class Sender : ISender
    {
        public bool SenderEmail(ConfigsEmail config, bool attachmentFile, bool withThreads, string filePath)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(config.Server, config.Port))
                {
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    smtpClient.EnableSsl = false;
                    MailMessage mailMessage = new MailMessage(config.Sender, config.To, config.Subject, config.Body);
                    if (attachmentFile)
                    {
                        Attachment attachment = new Attachment(config.Attachment);
                        mailMessage.Attachments.Add(attachment);
                    }
                    smtpClient.Send(mailMessage);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
