using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public static class IdFactory
    {
        private static readonly string _character = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static readonly string _characterMaskNumbersOnly = @"0123456789";
        private static readonly string _characterMaskNumbersAndUpperOnly = @"0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Regex _validatorNumbersAndUpperOnlyValidator = new(@"^[" + _characterMaskNumbersAndUpperOnly + "]+$");

        public static string RNGCharacterMask(int size) => RNGMask(_character, size);

        public static string RNGCharacterMaskNumbersOnly(int size) => RNGMask(_characterMaskNumbersOnly, size);

        public static string RNGCharacterMaskNumbersAndUpperOnly(int size) => RNGMask(_characterMaskNumbersAndUpperOnly, size);

        private static string RNGMask(string mask, int size)
        {
            char[] chars = new char[62];
            chars = mask.ToCharArray();
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            using RNGCryptoServiceProvider crypto = new();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
            byte[] data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        public static void RNGCharacterMaskNumbersAndUpperOnlyValidate(string val, int size)
        {
            //TODO: Hata eklenicek
          //  if (string.IsNullOrEmpty(val)) throw new Tools.CustomException(string.Format("Valide Etmek İstenen Değer Boş Olamaz"));
            //if (size <= 0) throw new Tools.CustomException(string.Format("Valide Etmek İstenen Değer Uzunluğu Boş Olamaz"));
            //if (val.Count() != size) throw new Tools.CustomException(string.Format("{0} Değer, {1} Karakter Olmalı", val, size));

            var isValid = _validatorNumbersAndUpperOnlyValidator.IsMatch(val);

            //if (isValid == false) throw new Tools.CustomException(string.Format("{0} Değeri Uygun Formatta Değil", val));
        }

        public static string GetId => RNGCharacterMask(25);
    }
}