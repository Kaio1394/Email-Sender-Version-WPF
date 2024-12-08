using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public interface ISender
    {
        public bool SenderEmail(ConfigsEmail config, bool attachmentFile, bool withThreads, string filePath);
    }
}
