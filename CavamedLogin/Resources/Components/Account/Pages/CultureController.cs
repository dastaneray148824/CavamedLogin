// Controllers/CultureController.cs
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]/[action]")]
public class CultureController : Controller
{
    // Controllers/CultureController.cs
    public IActionResult Set(string culture, string redirectUri)
    {
        if (culture != null)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                // BU SATIRI EKLEYEREK COOKIE'Yİ 1 YIL BOYUNCA GEÇERLİ KILIYORUZ
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
        }

        return LocalRedirect(redirectUri);
    }
}