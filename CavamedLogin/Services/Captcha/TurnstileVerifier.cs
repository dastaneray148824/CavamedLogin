using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace CavamedLogin.Services.Captcha;

public sealed class TurnstileVerifier : ICaptchaVerifier
{
    private readonly HttpClient _http;
    private readonly CaptchaOptions _opt;

    public TurnstileVerifier(HttpClient http, IOptions<CaptchaOptions> opt)
    { _http = http; _opt = opt.Value; }

    private sealed class Result
    {
        [JsonPropertyName("success")] public bool Success { get; set; }
        [JsonPropertyName("hostname")] public string? Hostname { get; set; }
        [JsonPropertyName("error-codes")] public string[]? ErrorCodes { get; set; }
    }

    public async Task<bool> VerifyAsync(string token, string? remoteIp, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;

        var form = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string,string>("secret",   _opt.SecretKey),
            new KeyValuePair<string,string>("response", token),
            new KeyValuePair<string,string>("remoteip", remoteIp ?? "")
        });

        using var resp = await _http.PostAsync(
            "https://challenges.cloudflare.com/turnstile/v0/siteverify", form, ct);
        resp.EnsureSuccessStatusCode();

        var stream = await resp.Content.ReadAsStreamAsync(ct);
        var result = await JsonSerializer.DeserializeAsync<Result>(stream, cancellationToken: ct);

        var hostOk = string.IsNullOrWhiteSpace(_opt.ExpectedHostname)
                     || string.Equals(result?.Hostname, _opt.ExpectedHostname, StringComparison.OrdinalIgnoreCase);

        return (result?.Success ?? false) && hostOk;
    }
}
