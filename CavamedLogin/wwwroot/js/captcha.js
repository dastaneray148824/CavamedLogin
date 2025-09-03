// wwwroot/js/captcha.js
(function () {
    'use strict';

    window.cavamed = window.cavamed || {};

    // .NET referansı kaydet
    window.cavamed.setDotNetRef = function (ref) {
        window.cavamed._ref = ref;
    };

    // Token'ı set et
    function setToken(token) {
        var el = document.getElementById('captchaTokenEmail');
        var v = token || '';

        if (el && el.value !== v) {
            el.value = v;
            el.dispatchEvent(new Event('input', { bubbles: true }));
        }

        if (window.cavamed._ref) {
            try { window.cavamed._ref.invokeMethodAsync('SetCaptchaToken', v); } catch { }
        }
    }

    // ✅ Turnstile callback’leri
    window.cavamed.onTurnstileSuccess = function (token) {
        if (!token || !token.length) {
            var cf = document.querySelector('input[name="cf-turnstile-response"]');
            if (cf && cf.value) token = cf.value;
        }
        setToken(token);
    };

    window.cavamed.onTurnstileExpired = function () {
        setToken('');
        var btn = document.getElementById('btnEmailNext');
        if (btn) btn.disabled = true;
    };

    // ✅ BURAYA EKLİYORSUN
    window.cavamed.resetCaptcha = function () {
        var host = document.querySelector('.cf-turnstile');
        if (window.turnstile && host) window.turnstile.reset();

        var hid = document.getElementById('captchaTokenEmail');
        if (hid) {
            hid.value = '';
            hid.dispatchEvent(new Event('input', { bubbles: true }));
        }

        if (window.cavamed._ref) {
            try { window.cavamed._ref.invokeMethodAsync('SetCaptchaToken', ''); } catch { }
        }
    };

})();
