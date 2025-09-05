using Enums;

namespace DTO
{
    public class LoginDTOSirket
    {
        public virtual string Id { get; set; }

        public virtual string Adi { get; set; }

        public virtual string Cont { get; set; }

        public virtual SirketKullaniciTipi KullaniciTipi { get; set; }

        public virtual string HesapPersonel_Id { get; set; }

        public virtual string HesapPersonel_Adi { get; set; }

        public virtual DomainSource DomainSource { get; set; }

        public virtual string AnaEkran { get; set; }

        public virtual string LogoURL { get; set; }
    }
}
