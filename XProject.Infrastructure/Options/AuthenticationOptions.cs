namespace XProject.Infrastructure.Options
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
