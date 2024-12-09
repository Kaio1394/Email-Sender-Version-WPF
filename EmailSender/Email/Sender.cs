using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NLog;

namespace EmailSender.Email
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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public bool SenderEmail(ConfigsEmail config, bool attachmentFile, string filePath)
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

        public bool SenderEmailWithThreads(ConfigsEmail config, List<string> listFiles, int qtdThreads)
        {
            Semaphore semaphore = new Semaphore(qtdThreads, qtdThreads);

            List<Thread> listThreads = new List<Thread>();
            Log.Info("Startando envio de e-mails com threads...");
            foreach (var file in listFiles)
            {
                Random rand = new Random();
                var thread = new Thread(() =>
                {
                    try
                    {
                        semaphore.WaitOne();
                        using (SmtpClient smtpClient = new SmtpClient(config.Server, config.Port))
                        {
                            smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                            smtpClient.EnableSsl = false;
                            MailMessage mailMessage = new MailMessage(config.Sender, config.To, config.Subject, config.Body);
                            Attachment attachment = new Attachment(file);
                            mailMessage.Attachments.Add(attachment);
                            smtpClient.Send(mailMessage);
                        }
                        Thread.Sleep(3000);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                });
                thread.Name = "Thread_" + rand.Next(1000000);
                listThreads.Add(thread);

            }
            foreach (var thread in listThreads)
            {
                Log.Info($"Startando a thread {thread.Name}");
                thread.Start();
            }
            while (listThreads.Any(x => x.ThreadState == ThreadState.Running || x.ThreadState == ThreadState.WaitSleepJoin))
            {
                Thread.Sleep(100);
            }
            return true;
        }
    }
}
