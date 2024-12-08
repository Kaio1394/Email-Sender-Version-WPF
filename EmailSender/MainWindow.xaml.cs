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
        private ConfigsEmail _configEmail;
        private ISender _sender;
        private Helper _helper;

        public MainWindow()
        {
            InitializeComponent();
            _sender = new Sender();
            _helper = new Helper();
        }

        internal void Button_Click(object sender, RoutedEventArgs e)
        {
            _configEmail = new ConfigsEmail()
            {
                Body = tbTo.Text,
                Subject = tbTo.Text,
                To = tbTo.Text,
                Port = !string.IsNullOrEmpty(tbPort.Text) ? Convert.ToInt32(tbPort.Text)
                                    : throw new ArgumentException("Invalid value port value!"),
                Sender = tbSender.Text,
                Server = tbServer.Text,
                Attachment = tbAttachment.Text
            };
            
            try
            {
                if (checkboxAttachment.IsChecked == true && !string.IsNullOrEmpty(_configEmail.Attachment))
                {
                    if(!Directory.Exists(_configEmail.Attachment))
                        throw new DirectoryNotFoundException(tbAttachment.Text);

                    _listFiles = _helper.GetListFiles(_configEmail.Attachment);
                    foreach (var file in _listFiles)
                    {
                        _sender.SenderEmail(_configEmail, true, false, file);
                    }
                }
                else if (checkboxAttachment.IsChecked == false && checkboxAttachment.IsChecked == false)
                {
                    _sender.SenderEmail(_configEmail, false, false, "");
                }
                else if (checkboxAttachment.IsChecked == false && 
                    checkboxThreads.IsChecked == false && 
                    checkboxThreads.IsChecked == true)
                {
                    foreach (var file in _listFiles)
                    {
                        _sender.SenderEmail(_configEmail, true, true, file);
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal void checkboxAttachment_Checked(object sender, RoutedEventArgs e)
        {
            tbAttachment.IsEnabled = true;
        }
        internal void checkboxAttachment_Unchecked(object sender, RoutedEventArgs e)
        {
            tbAttachment.IsEnabled = false;
        }

        internal void checkboxThreads_Checked(object sender, RoutedEventArgs e)
        {
            tbThreads.IsEnabled = true;
        }
        internal void checkboxThreads_Unchecked(object sender, RoutedEventArgs e)
        {
            tbThreads.IsEnabled = false;
        }
    }
}