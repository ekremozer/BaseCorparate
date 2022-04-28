using System;
using System.Collections.Generic;
using System.Text;

namespace BaseCorporate.Service.Model
{
    public class HeadModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Canonical { get; set; }
        public string OgUrl { get; set; }
        public string OgType { get; set; }
        public string OgTitle { get; set; }
        public string OgImage { get; set; }
        public string OgDescription { get; set; }
        public string OgSiteName { get; set; }
        public string SiteAddress { get; set; }
        public string TitleExtension { get; set; }
    }
}
