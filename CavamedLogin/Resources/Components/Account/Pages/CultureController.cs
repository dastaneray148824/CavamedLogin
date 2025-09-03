// Controllers/CultureController.cs
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

[Route("[controller]/[action]")]
public class CultureController : Controller
{

    // Controllers/CultureController.cs
    public IActionResult Set(string culture, string redirectUri)
    {
       
        if (culture != null)
        {
            //var cultureInfo = new CultureInfo(culture);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
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