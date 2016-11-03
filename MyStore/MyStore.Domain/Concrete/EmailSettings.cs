using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = ConfigurationManager.AppSettings["MailToAddress"];
        public string MailFromAddress = ConfigurationManager.AppSettings["MailFromAddress"];
        public bool UseSsl = bool.Parse(ConfigurationManager.AppSettings["UseSsl"]??"true");
        public string UserName = ConfigurationManager.AppSettings["UserName"];
        public string PassWord = ConfigurationManager.AppSettings["PassWord"];
        public string ServerName = ConfigurationManager.AppSettings["ServerName"];
        public int ServerPort = int.Parse(ConfigurationManager.AppSettings["ServerPort"]??"587");
    }
}
