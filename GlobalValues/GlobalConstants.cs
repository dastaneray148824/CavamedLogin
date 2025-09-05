using System;

namespace GlobalValues
{
    public static class GlobalConstants
    {
        public const string _sistemUserInfo = "sis";
        public static string apiURL => "https://api.cavamed.net/api";

        /// <summary>
        /// Detecting Application Cavamed or Erpimus
        /// </summary>
        public static bool isCavamed = true;
        public static string ProductName(Enums.DomainSource domain)
        {
            var url = "Erpimus E-Business Suite";
            switch (domain)
            {
                case Enums.DomainSource.Erpimus: break;
                case Enums.DomainSource.Cavamed: url = "CAVAMED E-Business Suite"; break;
                default: break;
            }

            return url;
        }
        public static string ProductAuther(Enums.DomainSource domain)
        {
            var url = "Erpiks Bilişim &copy; 2016 - " + DateTime.Today.Year;
            switch (domain)
            {
                case Enums.DomainSource.Erpimus: break;
                case Enums.DomainSource.Cavamed: url = "CAVAMED &copy; 2021 - " + DateTime.Today.Year; break;
                default: break;
            }

            return url;
        }

        //QR Parameters
        public static string _qrValue = "qrValue";
        public static string _qrPropertyName = "qrPropertyName";
        public static string _qrPropertyName_Id = "qrPropertyNameId";
        public static string _qrValue_Id = "qrValueId";
        public static string _qrPropertyName_Kodu = "qrPropertyNameKodu";
        public static string _qrValue_Kodu = "qrValueKodu";
    }
}