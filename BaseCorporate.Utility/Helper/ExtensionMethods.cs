using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace BaseCorporate.Utility.Helper
{
    public static class ExtensionMethods
    {
        public static string GetUniqueFileName(string fileName, bool addGuid = true)
        {
            var fileExtension = Path.GetExtension(fileName);
            fileName = Path.GetFileNameWithoutExtension(fileName).ToUrl();
            var uniqueFleName = addGuid ?
                $"{Path.GetFileNameWithoutExtension(fileName)}-{Guid.NewGuid().ToString()[..4]}{fileExtension}"
                : $"{Path.GetFileNameWithoutExtension(fileName)}{fileExtension}";
            return uniqueFleName;
        }

        public static string ToUrl(this string toUrl)
        {
            if (string.IsNullOrEmpty(toUrl)) return string.Empty;
            toUrl = toUrl.ToLower();
            toUrl = toUrl.Replace("ş", "s");
            toUrl = toUrl.Replace("ğ", "g");
            toUrl = toUrl.Replace("ı", "i");
            toUrl = toUrl.Replace("ç", "c");
            toUrl = toUrl.Replace("ö", "o");
            toUrl = toUrl.Replace("ü", "u");
            toUrl = toUrl.Replace("'", "");
            toUrl = toUrl.Replace("\"", "");
            var regex = new Regex("[^a-zA-Z0-9_-]");
            toUrl = regex.Replace(toUrl, "-");
            toUrl = toUrl.Replace("--", "-");
            if (!string.IsNullOrEmpty(toUrl))
            {
                while (toUrl.IndexOf("--", StringComparison.Ordinal) > -1)
                {
                    toUrl = toUrl.Replace("--", "-");
                }
            }
            if (toUrl.Length > 200) toUrl = toUrl.Substring(0, 200);
            if (toUrl.StartsWith("-")) toUrl = toUrl.Substring(1);
            if (toUrl.EndsWith("-")) toUrl = toUrl.Substring(0, toUrl.Length - 1);
            return toUrl;
        }

        public static T GetObjectValue<T>(this object obj)
        {
            try
            {
                if (typeof(T) == typeof(bool))
                {
                    var isBoolValue = obj.ToString() == "1" || obj.ToString()?.ToLower() == "true";
                    return (T)Convert.ChangeType(isBoolValue, typeof(T));
                }
                if (typeof(T) == typeof(Guid))
                {
                    var isGuidValue = Guid.TryParse(obj.ToString(), out var guidValue);
                    return (T)Convert.ChangeType(isGuidValue ? guidValue : Guid.Empty, typeof(T));
                }
                if (typeof(T) == typeof(Guid?))
                {

                    var isGuidValue = Guid.TryParse(obj.ToString(), out var guidValue);
                    return (T)Convert.ChangeType(isGuidValue ? guidValue : (Guid?)null, typeof(T));
                }
                return (T)Convert.ChangeType(obj.ToString(), typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static string GetFancyboxLink(string url, string cssClass, int width, int height, string text)
        {
            var link = "<a data-src=\"{Url}\" class=\"{Class}\" href=\"javascript:;\" data-type=\"{DataType}\" data-fancybox data-options='{\"type\" : \"iframe\", \"iframe\" : {\"preload\" : false, \"css\" : {\"width\" : \"{Width}px\",\"height\" : \"{Height}px\"}}}'>{Text}</a>";
            link = link.Replace("{Url}", url)
                .Replace("{Class}", cssClass)
                .Replace("{Width}", width.ToString())
                .Replace("{Height}", height.ToString())
                .Replace("{Text}", text);
            return link;
        }

        public static string GetDeleteConfirm(string function, string cssClass, string text)
        {
            var link = "<a href=\"javascript:void(0)\" {Function} class=\"btn btn-danger waves-effect {Class}\">{Text}</a>";
            link = link.Replace("{Function}", function)
                .Replace("{Class}", cssClass)
                .Replace("{Text}", text);
            return link;
        }

        public static string Pager(int totalPage, int currentPage, string slug)
        {
            if (totalPage == 1) return null;
            var isSearchPage = slug.Contains("ara?q=");
            var sbPagingHtml = new StringBuilder();

            var prevTag = "<li class='prev-page'><a href='{href}'>&laquo;</a></li>";
            prevTag = !isSearchPage ?
            prevTag.Replace("{href}", currentPage > 1 ? $"{slug}?page={currentPage - 1}" : "javascript:;") :
            //prevTag.Replace("{href}", currentPage > 1 ? $"{slug}/{currentPage - 1}" : "javascript:;") :
            prevTag.Replace("{href}", currentPage > 1 ? $"{slug}&page={currentPage - 1}" : "javascript:;");

            sbPagingHtml.Append(prevTag);

            int forStartIndex;
            int forTotalCount;

            if (totalPage <= 7)
            {
                forStartIndex = 1;
                forTotalCount = totalPage;
            }
            else
            {
                if (currentPage + 6 < totalPage)
                {
                    forStartIndex = currentPage;
                    forTotalCount = currentPage + 6;
                }
                else
                {
                    forStartIndex = totalPage - 6;
                    forTotalCount = totalPage;
                }
            }

            var pageTag = "<li><a href='{href}' {class}>{text}</a></li>";
            for (var i = forStartIndex; i <= forTotalCount; i++)
            {
                var tag = pageTag.Replace("{text}", i.ToString());
                if (currentPage == i)
                {
                    tag = tag.Replace("{href}", "javascript:;");
                    tag = tag.Replace("{class}", "class='active'");
                }
                else
                {
                    tag = !isSearchPage ? tag.Replace("{href}", $"{slug}?page={i}") : tag.Replace("{href}", $"{slug}&page={i}");
                    tag = tag.Replace("{class}", string.Empty);
                }
                sbPagingHtml.Append(tag);
            }

            var nexTag = "<li class='next-page'><a href='{href}'>&raquo;</a></li>";
            nexTag = !isSearchPage ?
                nexTag.Replace("{href}", currentPage < forTotalCount ? $"{slug}?page={currentPage + 1}" : "javascript:;") :
                nexTag.Replace("{href}", currentPage < forTotalCount ? $"{slug}&page={currentPage + 1}" : "javascript:;");

            sbPagingHtml.Append(nexTag);

            return sbPagingHtml.ToString();
        }

        public static string BodySummaryWithLength(this string body, int length = 300)
        {
            var regexAllTags = new Regex(@"<[^>]*>");
            var regexIsTag = new Regex(@"<|>");
            var regexOpen = new Regex(@"<[^/][^>]*>");
            var regexClose = new Regex(@"</[^>]*>");
            var regexAttribute = new Regex(@"<[^ ]*");
            var necessaryCount = 0;

            if (regexAllTags.Replace(body, "").Length <= length)
            {
                return body;
            }
            var split = regexAllTags.Split(body);
            var counter = string.Empty;

            foreach (var item in split)
            {
                if (counter.Length < length && counter.Length + item.Length >= length)
                {
                    necessaryCount = body.IndexOf(item, counter.Length, StringComparison.Ordinal) + item.Substring(0, length - counter.Length).Length;
                    break;
                }
                counter += item;
            }

            var x = regexIsTag.Match(body, necessaryCount);
            if (x.Value == ">")
            {
                necessaryCount = x.Index + 1;
            }

            var bodySummary = body.Substring(0, necessaryCount);
            var openTags = regexOpen.Matches(bodySummary);
            var closeTags = regexClose.Matches(bodySummary);

            var newOpenTags = new List<string>();
            foreach (var item in openTags)
            {
                var trans = regexAttribute.Match(item.ToString()).Value;

                if (trans.Last() == '>')
                {
                    trans = "</" + trans.Substring(1, trans.Length - 1);
                }
                else
                {
                    trans = "</" + trans.Substring(1, trans.Length - 1) + ">";
                }

                newOpenTags.Add(trans);
            }

            foreach (Match close in closeTags)
            {
                newOpenTags.Remove(close.Value);
            }

            for (var i = newOpenTags.Count - 1; i >= 0; i--)
            {
                bodySummary += newOpenTags[i];
            }

            bodySummary = bodySummary.Replace("</br>", string.Empty);

            return bodySummary;
        }

        public static string BodySummaryWithPageBreak(string body)
        {
            if (body.IndexOf("<div style=\"page-break-after: always\">", StringComparison.Ordinal) > -1)
            {
                body = body.Substring(0, body.IndexOf("<div style=\"page-break-after: always\">", StringComparison.Ordinal));
                return body;
            }
            return body;
        }

        public static string GetBreadCrumbListElement(string link, string linkText, string linkIndex)
        {
            var sbBreadCrumbListElement = new StringBuilder();
            sbBreadCrumbListElement.Append("<li itemprop=\"itemListElement\" itemscope=\"\" itemtype=\"http://schema.org/ListItem\">");
            sbBreadCrumbListElement.Append($"<a itemprop=\"item\" href=\"{link}\"><span itemprop=\"name\">{linkText}</span></a>");
            sbBreadCrumbListElement.Append($"<meta itemprop=\"position\" content=\"{linkIndex}\">");
            sbBreadCrumbListElement.Append("</li>");
            return sbBreadCrumbListElement.ToString();
        }

        public static string ClearPhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;
            var phoneNumberCharArray = phoneNumber.Where(char.IsNumber).ToArray();
            phoneNumber = new string(phoneNumberCharArray);
            phoneNumber = phoneNumber.StartsWith("00") ? phoneNumber.Substring(2) : phoneNumber;
            phoneNumber = phoneNumber.StartsWith("90") ? phoneNumber.Substring(2) : phoneNumber;
            phoneNumber = phoneNumber.StartsWith("0") ? phoneNumber.Substring(1) : phoneNumber;
            phoneNumber = phoneNumber.Trim();
            return phoneNumber;
        }

        public static string HidePhoneNumber(this string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;
            var last = phoneNumber.ClearPhoneNumber().Substring(6, 4);
            phoneNumber = $"*** *** {last}";
            return phoneNumber;
        }

        public static decimal DecimalFix(this decimal? value, int fraction = 2)
        {
            if (value == null)
            {
                return 0;
            }
            return decimal.Round((decimal)value, fraction, MidpointRounding.AwayFromZero);
        }

        public static string NewLineToBrTag(this string content)
        {
            content = content.Replace(Environment.NewLine, "<br/>");
            return content;
        }

        public static string SmsPassword()
        {
            return new Random().Next(123456, 999999).ToString();
        }

        public static string ClearTurkishCharacter(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            text = text
                .Replace("Ğ", "G")
                .Replace("Ü", "U")
                .Replace("Ş", "S")
                .Replace("İ", "i")
                .Replace("Ö", "O")
                .Replace("Ç", "C")
                .Replace("ğ", "g")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ç", "c");

            while (text.IndexOf("  ", StringComparison.Ordinal) > -1)
            {
                text = text.Replace("  ", " ");
            }

            text = text.Trim();
            return text;
        }

        public static string ToDigitsOnly(this string text)
        {
            var digitsOnly = new Regex(@"[^\d]");
            return digitsOnly.Replace(text, string.Empty);
        }

        public static string ClearHtml(this string text)
        {
            return Regex.Replace(text, @"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", string.Empty);
        }
        
    }
}
