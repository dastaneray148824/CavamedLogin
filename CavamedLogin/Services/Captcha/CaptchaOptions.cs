namespace CavamedLogin.Services.Captcha
{
    public sealed class CaptchaOptions
    {
        public string Provider { get; set; } = "Turnstile";
        public string SiteKey { get; set; } = "";
        public string SecretKey { get; set; } = "";
        public string? ExpectedHostname { get; set; }
    }
}
