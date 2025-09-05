using Enums;

namespace DTO
{
    public class LoginDTO
    {
        public virtual string AdiSoyadi { get; set; }
        public virtual string TokenId { get; set; }

        public virtual LoginType LoginType { get; set; }

        public virtual string EMail { get; set; }

        public virtual string Sifre { get; set; }

        public virtual string KullaniciIP { get; set; }

        public virtual BrowserEngine? BrowserEngine { get; set; }

        public virtual BrowserName? BrowserName { get; set; }

        public virtual string UserAgent { get; set; }

        public virtual KullaniciCihaz KullaniciCihaz { get; set; }

        public virtual Lang Lang { get; set; }

        public virtual List<LoginDTOSirket> SirketList { get; set; }

        public LoginDTO()
        {
            SirketList = new List<LoginDTOSirket>();
        }

    }
}
