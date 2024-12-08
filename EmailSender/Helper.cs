using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender
{
    public class Helper
    {
        public List<string> GetListFiles(string pathDir)
        {
            DirectoryInfo dir = new DirectoryInfo(pathDir);
            return dir.GetFiles().Select(x => x.FullName).ToList();
        }
    }
}
