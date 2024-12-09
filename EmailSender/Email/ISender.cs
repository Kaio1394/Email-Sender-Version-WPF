using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Email
{
    public interface ISender
    {
        public bool SenderEmail(ConfigsEmail config, bool attachmentFile, string filePath);
        public bool SenderEmailWithThreads(ConfigsEmail config, List<string> listFiles, int qtdThreads);
    }
}
