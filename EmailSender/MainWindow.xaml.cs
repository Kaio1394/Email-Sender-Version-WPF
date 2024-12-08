using System.Net.Mail;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace EmailSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<string> _listFiles = new List<string>();
        private string _tbSender;
        private string _tbTo;
        private string _tbSubject;
        private string _tbBody;
        private string _tbServer;
        private string _tbAttachement;
        private int _tbPort;

        public MainWindow()
        {
            InitializeComponent();
        }
        private List<string> GetListFiles(string pathDir)
        {
            DirectoryInfo dir = new DirectoryInfo(pathDir);
            return dir.GetFiles().Select(x => x.FullName).ToList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _tbAttachement = tbAttachment.Text;
            _tbSender = tbSender.Text;
            _tbTo = tbTo.Text;
            _tbSubject = tbTo.Text;
            _tbBody = tbTo.Text;
            _tbServer = tbServer.Text;
            _tbPort = !string.IsNullOrEmpty(tbPort.Text) ? Convert.ToInt32(tbPort.Text)
                : throw new ArgumentException("Invalid value port value!");
            try
            {
                if (checkboxAttachment.IsChecked == true && !string.IsNullOrEmpty(_tbAttachement))
                {
                    if(!Directory.Exists(_tbAttachement))
                        throw new DirectoryNotFoundException(tbAttachment.Text);

                    _listFiles = GetListFiles(_tbAttachement);
                    foreach (var file in _listFiles)
                    {
                        using (SmtpClient smtpClient = new SmtpClient(_tbServer, _tbPort))
                        {
                            smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                            smtpClient.EnableSsl = false;
                            MailMessage mailMessage = new MailMessage(_tbSender, _tbTo, _tbSubject, _tbBody);
                            Attachment attachment = new Attachment(file);
                            mailMessage.Attachments.Add(attachment);
                            smtpClient.Send(mailMessage);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void checkboxAttachment_Checked(object sender, RoutedEventArgs e)
        {
            tbAttachment.IsEnabled = true;
        }
        private void checkboxAttachment_Unchecked(object sender, RoutedEventArgs e)
        {
            tbAttachment.IsEnabled = false;
        }

        private void checkboxThreads_Checked(object sender, RoutedEventArgs e)
        {
            tbThreads.IsEnabled = true;
        }
        private void checkboxThreads_Unchecked(object sender, RoutedEventArgs e)
        {
            tbThreads.IsEnabled = false;
        }
    }
}