using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseCorporate.Service.Abstract;
using BaseCorporate.Utility.Helper;
using BaseCorporate.Utility.Model;

namespace BaseCorporate.Web.Infrastructure
{
    public static class AppParameterBinding
    {
        public static void AllSetting(ISettingService settingService)
        {
            MailSetting(settingService);
            MetaSetting(settingService);
        }
        public static void MailSetting(ISettingService settingService)
        {
            var smsSetting = settingService.GetList("mail");
            AppParameter.MailSetting = new MailSetting();
            AppParameter.MailSetting.MailSender = smsSetting.FirstOrDefault(x => x.Key == "MailSender")?.Value;
            AppParameter.MailSetting.SmtpMail = smsSetting.FirstOrDefault(x => x.Key == "SmtpMail")?.Value;
            AppParameter.MailSetting.EditorMail = smsSetting.FirstOrDefault(x => x.Key == "EditorMail")?.Value;
            AppParameter.MailSetting.AdminMail = smsSetting.FirstOrDefault(x => x.Key == "AdminMail")?.Value;
            AppParameter.MailSetting.MailPass = smsSetting.FirstOrDefault(x => x.Key == "MailPass")?.Value;
            AppParameter.MailSetting.Port = smsSetting.FirstOrDefault(x => x.Key == "Port")?.Value;
            AppParameter.MailSetting.Host = smsSetting.FirstOrDefault(x => x.Key == "Host")?.Value;
            AppParameter.MailSetting.EnableSsl = smsSetting.FirstOrDefault(x => x.Key == "Host")?.Value == "1" || smsSetting.FirstOrDefault(x => x.Key == "Host")?.Value.ToLower() == "true";
        }

        public static void MetaSetting(ISettingService settingService)
        {
            var metaSetting = settingService.GetList("meta");
            AppParameter.MetaSetting = new MetaSetting();
            AppParameter.MetaSetting.SiteAddress = metaSetting.FirstOrDefault(x => x.Key == "SiteAddress")?.Value;
            AppParameter.MetaSetting.Title = metaSetting.FirstOrDefault(x => x.Key == "Title")?.Value;
            AppParameter.MetaSetting.TitleExtension = metaSetting.FirstOrDefault(x => x.Key == "TitleExtension")?.Value;
            AppParameter.MetaSetting.MetaDescription = metaSetting.FirstOrDefault(x => x.Key == "MetaDescription")?.Value;
            AppParameter.MetaSetting.MetaKeywords = metaSetting.FirstOrDefault(x => x.Key == "MetaKeywords")?.Value;
            AppParameter.MetaSetting.Author = metaSetting.FirstOrDefault(x => x.Key == "Author")?.Value;
            AppParameter.MetaSetting.OgDescription = metaSetting.FirstOrDefault(x => x.Key == "OgDescription")?.Value;
            AppParameter.MetaSetting.OgImage = metaSetting.FirstOrDefault(x => x.Key == "OgImage")?.Value;
            AppParameter.MetaSetting.OgSiteName = metaSetting.FirstOrDefault(x => x.Key == "OgSiteName")?.Value;
            AppParameter.MetaSetting.OgTitle = metaSetting.FirstOrDefault(x => x.Key == "OgTitle")?.Value;
            AppParameter.MetaSetting.OgType = metaSetting.FirstOrDefault(x => x.Key == "OgType")?.Value;
        }
    }
}
