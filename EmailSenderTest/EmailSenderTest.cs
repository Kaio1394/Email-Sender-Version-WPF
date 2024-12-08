
using EmailSender;
using Moq;

namespace EmailSenderTest
{
    public class EmailSenderTest
    {
        private MainWindow _mainWindow;
        private Mock<ISender> _mockSender;
        private Mock<Helper> _mockHelper;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
            _mainWindow = new MainWindow();
            _mockHelper = new Mock<Helper>();
        }

        [Test]
        public void EmailSender_()
        {
            _mockSender.Setup(x => x.SenderEmail(It.IsAny<ConfigsEmail>(), false, false, It.IsAny<string>()))
                .Returns(true);
            _mockHelper.Setup(x => x.GetListFiles(It.IsAny<string>()))
                .Returns(new List<string>
                {

                });
        }
    }
}