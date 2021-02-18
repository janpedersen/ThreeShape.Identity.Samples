using System;

namespace OAuthWpfApp.Login
{
    public class UserClaims
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Ip { get; set; }
        public string CompanyId { get; set; }
        public string Roles { get; set; }
        public DateTime TokenExpiresAt { get; set; }
        public string Token { get; set; }
    }
}
