using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DTO
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Accepts a Unix/PHP date string format and returns a valid .NET date format
        /// </summary>
        /// <param name="format">The PHP format string</param>
        /// <returns>The format string converted to .NET DateTime format specifiers</returns>
        public static string ConvertPHPToNet(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return "";
            }

            StringBuilder final = new StringBuilder(128);
            string temp = "";
            Match m = Regex.Match(format, @"(%|\\)?.|%%", RegexOptions.IgnoreCase);

            while (m.Success)
            {
                temp = m.Value;

                if (temp.StartsWith(@"\") || temp.StartsWith("%%"))
                {
                    final.Append(temp.Replace(@"\", "").Replace("%%", "%"));
                }

                switch (temp)
                {
                    case "d":
                        final.Append("dd");
                        break;
                    case "D":
                        final.Append("ddd");
                        break;
                    case "j":
                        final.Append("d");
                        break;
                    case "l":
                        final.Append("dddd");
                        break;
                    case "F":
                        final.Append("MMMM");
                        break;
                    case "m":
                        final.Append("MM");
                        break;
                    case "M":
                        final.Append("MMM");
                        break;
                    case "n":
                        final.Append("M");
                        break;
                    case "Y":
                        final.Append("yyyy");
                        break;
                    case "y":
                        final.Append("yy");
                        break;
                    case "a":
                    case "A":
                        final.Append("tt");
                        break;
                    case "g":
                        final.Append("h");
                        break;
                    case "G":
                        final.Append("H");
                        break;
                    case "h":
                        final.Append("hh");
                        break;
                    case "H":
                        final.Append("HH");
                        break;
                    case "i":
                        final.Append("mm");
                        break;
                    case "s":
                        final.Append("ss");
                        break;
                    default:
                        final.Append(temp);
                        break;
                }
                m = m.NextMatch();
            }

            return final.ToString();
        }

        /// <summary>
        /// Accepts a Unix/PHP date string format and returns a valid .NET date format
        /// </summary>
        /// <param name="format">The .NET format string</param>
        /// <returns>The format string converted to PHP format specifiers</returns>
        public static string ConvertNetToPHP(string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return "";
            }

            StringBuilder final = new StringBuilder(128);
            string temp = "";

            switch (format.Trim())
            {
                case "d":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    break;
                case "D":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;
                    break;
                case "t":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
                    break;
                case "T":
                    format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongTimePattern;
                    break;
            }

            Match m = Regex.Match(format, @"(\\)?(dd?d?d?|MM?M?M?|yy?y?y?|hh?|HH?|mm?|ss?|tt?|S)|.", RegexOptions.IgnoreCase);

            while (m.Success)
            {
                temp = m.Value;

                switch (temp)
                {
                    case "dd":
                        final.Append("d");
                        break;
                    case "ddd":
                        final.Append("D");
                        break;
                    case "d":
                        final.Append("j");
                        break;
                    case "dddd":
                        final.Append("l");
                        break;
                    case "MMMM":
                        final.Append("F");
                        break;
                    case "MM":
                        final.Append("m");
                        break;
                    case "MMM":
                        final.Append("M");
                        break;
                    case "M":
                        final.Append("n");
                        break;
                    case "yyyy":
                        final.Append("Y");
                        break;
                    case "yy":
                        final.Append("y");
                        break;
                    case "tt":
                        final.Append("a");
                        break;
                    case "h":
                        final.Append("g");
                        break;
                    case "H":
                        final.Append("G");
                        break;
                    case "hh":
                        final.Append("h");
                        break;
                    case "HH":
                        final.Append("H");
                        break;
                    case "mm":
                        final.Append("i");
                        break;
                    case "ss":
                        final.Append("s");
                        break;
                    default:
                        final.Append(temp);
                        break;
                }
                m = m.NextMatch();
            }

            return final.ToString();
        }

        public static string ConvertNetToMySQL(string format) => string.Format("%{0}", ConvertNetToPHP(format).Replace(".", ".%").Replace(":", ":%"));

        private static readonly long DatetimeMinTimeTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        public static long ToJavaScriptMilliseconds(DateTime dt)
        {
            return (long)((dt.ToUniversalTime().Ticks - DatetimeMinTimeTicks) / 10000);
        }

        public static string TimeSpanToYearMonthDaySinceToday(DateTime? date)
        {
            if (date.HasValue == false)
                return "";

            DateTime today = DateTime.Now;

            if (today < date.Value)
                return "";

            var val = date.Value;

            TimeSpan span = today - val;

            DateTime age = DateTime.MinValue + span;

            int Years = age.Year - 1;
            int Months = age.Month - 1;
            int Days = age.Day - 1;
            return Years.ToString() + " Yıl, " + Months.ToString() + " Ay, " + Days.ToString() + " Gün";
        }

        public static DateTime? MergeDateAndTime(this DateTime? dt, TimeSpan? ts) => (dt == null || ts == null) ? (DateTime?)null : MergeDateAndTime(dt.Value, ts.Value);
        public static DateTime MergeDateAndTime(this DateTime dt, TimeSpan ts) => new DateTime(dt.Year, dt.Month, dt.Day, ts.Hours, ts.Minutes, ts.Seconds);

        public static string ToShortDateString(this DateTime? dt) => dt.HasValue ? dt.Value.ToShortDateString() : "";

        public static string ToLongDateString(this DateTime? dt) => dt.HasValue ? dt.Value.ToLongDateString() : "";
        public static string ToLongDateTimeString(this DateTime? dt) => dt.HasValue ? dt.Value.ToLongDateTimeString() : "";
        public static string ToLongDateTimeSecondsString(this DateTime? dt) => dt.HasValue ? dt.Value.ToLongDateTimeSecondsString() : "";

        public static string ToLongDateString(this DateTime dt) => dt.ToLongDateString();
        public static string ToLongDateTimeString(this DateTime dt) => string.Format("{0} {1}", dt.ToShortDateString(), dt.ToShortTimeString());
        public static string ToLongDateTimeSecondsString(this DateTime dt) => string.Format("{0} {1}", dt.ToShortDateString(), dt.ToLongTimeString());

        public static string TimeAgo(this DateTime? dt) => dt.HasValue ? DateTime.Now.Subtract(dt.Value).TimeAgo() : "-";
        public static string TimeAgo(this DateTime dt) => DateTime.Now.Subtract(dt).TimeAgo();
        public static string TimeAgo(this TimeSpan timeSpan)
        {
            string result = string.Empty;

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} saniye", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ? string.Format("yaklaşık {0} dakika", timeSpan.Minutes) : "yaklaşık bir dakika";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ? string.Format("yaklaşık {0} saat", timeSpan.Hours) : "yaklaşık bir saat";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? string.Format("yaklaşık {0} gün", timeSpan.Days) : "dün";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ? string.Format("yaklaşık {0} ay", timeSpan.Days / 30) : "yaklaşık bir ay";
            }
            else
            {
                result = timeSpan.Days > 365 ? string.Format("yaklaşık {0} yıl", timeSpan.Days / 365) : "yaklaşık bir yıl";
            }

            return result;
        }

        public static string ToReadableString(this TimeSpan? span) { if (span == null) return ""; return span.Value.ToReadableString(); }
        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} gün, ", span.Days) : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} saat, ", span.Hours) : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} dakika, ", span.Minutes) : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} saniye", span.Seconds) : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "";

            return formatted;
        }

        static GregorianCalendar _gc = new GregorianCalendar();

        public static int GetWeekOfMonth(this DateTime time)
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        public static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static DateTime GetUTCTime()
        {
            //default Windows time server
            const string ntpServer = "time.windows.com";

            // NTP message size - 16 bytes of the digest (RFC 2030)
            var ntpData = new byte[48];

            //Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //The UDP port number assigned to NTP is 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP uses UDP

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //*UTC* time
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime;
        }

        public static DateTime GetLocalTime()
        {
            return GetUTCTime().ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) + ((x & 0x0000ff00) << 8) + ((x & 0x00ff0000) >> 8) + ((x & 0xff000000) >> 24));
        }

        /// <summary>
        /// Girilen Tarihin İçinde Bulunduğu Ayın Son Gününü Döner
        /// Tarih Boş ise Bugün kabul edilir
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime? date = null)
        {
            if (date == null) date = DateTime.Today;
            var val = date.Value;

            return new DateTime(val.Year, val.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Girilen Tarihin İçinde Bulunduğu Ayın İlk Gününü Döner
        /// TArih Boş iese Bugün kabul edilir
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime? date = null)
        {
            if (date == null) date = DateTime.Today;
            var val = date.Value;

            return new DateTime(val.Year, val.Month, 1);
        }

        public static List<DateTime> GetDatesOfMonth(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }

        public static Enums.Gun DayOfWeekToGun(DayOfWeek val)
        {
            switch (val)
            {
                case DayOfWeek.Sunday: return Enums.Gun.Pazar;
                case DayOfWeek.Monday: return Enums.Gun.Pazartesi;
                case DayOfWeek.Tuesday: return Enums.Gun.Sali;
                case DayOfWeek.Wednesday: return Enums.Gun.Carsamba;
                case DayOfWeek.Thursday: return Enums.Gun.Persembe;
                case DayOfWeek.Friday: return Enums.Gun.Cuma;
                case DayOfWeek.Saturday: return Enums.Gun.Cumartesi;
                default: return Enums.Gun.Pazar;
            }
        }

        public static DayOfWeek GunToDayOfWeek(Enums.Gun val)
        {
            switch (val)
            {
                case Enums.Gun.Pazartesi: return DayOfWeek.Monday;
                case Enums.Gun.Sali: return DayOfWeek.Tuesday;
                case Enums.Gun.Carsamba: return DayOfWeek.Wednesday;
                case Enums.Gun.Persembe: return DayOfWeek.Thursday;
                case Enums.Gun.Cuma: return DayOfWeek.Friday;
                case Enums.Gun.Cumartesi: return DayOfWeek.Saturday;
                case Enums.Gun.Pazar: return DayOfWeek.Sunday;
                default: return DayOfWeek.Sunday;
            }
        }
    }
}
