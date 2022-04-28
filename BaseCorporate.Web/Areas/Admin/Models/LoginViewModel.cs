namespace BaseCorporate.Web.Areas.Admin.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ErrorMessage { get; set; }
        public string IpAddress { get; set; }
    }
}
