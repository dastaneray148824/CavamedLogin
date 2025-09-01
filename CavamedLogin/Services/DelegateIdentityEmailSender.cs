using CavamedLogin.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

class DelegateIdentityEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailSender _inner;
    public DelegateIdentityEmailSender(IEmailSender inner) => _inner = inner;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string link)
        => _inner.SendEmailAsync(email, "Confirm your email",
            $"E-postanızı doğrulamak için <a href=\"{link}\">buraya tıklayın</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string link)
        => _inner.SendEmailAsync(email, "Reset your password",
            $"Şifrenizi sıfırlamak için <a href=\"{link}\">buraya tıklayın</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string code)
        => _inner.SendEmailAsync(email, "Password reset code",
            $"Kodunuz: <b>{code}</b>");
}