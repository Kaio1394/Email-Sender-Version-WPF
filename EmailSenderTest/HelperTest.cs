using EmailSender;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderTest
{
    internal class HelperTest
    {
        private Helper _helper;
        const string DIR_FILES = "C:\\temp\\ArquivosTeste";
        const string FILE_1 = DIR_FILES + "\\file1.txt";
        const string FILE_2 = DIR_FILES + "\\file2.txt";

        [SetUp]
        public void Setup()
        {
            _helper = new Helper();
            Directory.CreateDirectory(DIR_FILES);
            File.Create(FILE_1);
            File.Create(FILE_2);
        }
        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(DIR_FILES))
                Directory.Delete(DIR_FILES);
        }

        [Test]
        public void Helper_GetFiles()
        {
            var listFiles = _helper.GetListFiles(DIR_FILES);
            Assert.That(listFiles.Count == 2);
        }
    }
}
