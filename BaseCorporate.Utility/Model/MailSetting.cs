using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Utility.Model
{
    public class MailSetting
    {
        public string MailSender { get; set; }
        public string SmtpMail { get; set; }
        public string EditorMail { get; set; }
        public string AdminMail { get; set; }
        public string MailPass { get; set; }
        public string Port { get; set; }
        public string Host { get; set; }
        public bool EnableSsl { get; set; }
    }
}
