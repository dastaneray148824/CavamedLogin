namespace CavamedLogin.Services.Captcha
{
    public interface ICaptchaVerifier
    {
        Task<bool> VerifyAsync(string token, string? remoteIp, CancellationToken ct = default);
    }
}
