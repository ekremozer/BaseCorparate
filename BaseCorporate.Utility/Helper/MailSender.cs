using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BaseCorporate.Utility.Helper
{
    public static class MailSender
    {
        public static void MailSendDefaultAccount(string sendMail, string subject, string body, string cc = "", Attachment attachment = null)
        {
            Task.Run(() =>
            {
                var smtpMail = AppParameter.MailSetting.SmtpMail;
                var mailSender = AppParameter.MailSetting.MailSender;
                sendMail = string.IsNullOrEmpty(sendMail) ? smtpMail : sendMail;

                if (string.IsNullOrEmpty(smtpMail))
                {
                    return;
                }
                var eMail = new MailMessage
                {
                    From = new MailAddress(smtpMail, mailSender)
                };

                eMail.To.Add(sendMail);
                eMail.Subject = subject;
                eMail.IsBodyHtml = true;
                eMail.Body = body.NewLineToBrTag();
                if (attachment != null)
                {
                    eMail.Attachments.Add(attachment);
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    cc = cc.Replace(",", ";");
                    if (cc.Contains(";"))
                    {
                        var ccArray = cc.Split(';');
                        foreach (var c in ccArray)
                        {
                            if (c.IsValidEmail())
                            {
                                eMail.CC.Add(c);
                            }
                        }
                    }
                    else
                    {

                        if (cc.IsValidEmail())
                        {
                            eMail.CC.Add(cc);
                        }
                    }
                }

                var smtp = GetDefaultSmtp();
                try
                {
                    smtp.Send(eMail);
                }
                catch (Exception ex)
                {
                    //ErrorLog
                }
                finally
                {
                    eMail.Dispose();
                    smtp.Dispose();
                }
            });
           
        }

        public static SmtpClient GetDefaultSmtp()
        {
            var email = AppParameter.MailSetting.SmtpMail;
            var password = AppParameter.MailSetting.MailPass;
            int.TryParse(AppParameter.MailSetting.Port, out var port);
            var host = AppParameter.MailSetting.Host ?? string.Empty;
            var enableSsl = AppParameter.MailSetting.EnableSsl;
            var smtp = new SmtpClient
            {
                Credentials = new NetworkCredential(email, password),
                Port = port,
                Host = host,
                EnableSsl = enableSsl
            };
            return smtp;
        }
    }
}
