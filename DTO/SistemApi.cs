using System.Collections.Generic;
using Enums;

namespace DTO
{
    public class SistemApi
    {
        public virtual string Sirket_Id { get; set; }

        public LoginType LoginType { get; set; }

        public string ApiName { get; set; }

        public SistemModul SistemModul { get; set; }

        public SistemModulGrup SistemModulGrup { get; set; }

        public string BagliApi { get; set; }

        public List<string> BagliApiList { get; set; }

        public bool InPortal { get; set; }

        public bool InMobile { get; set; }

        public bool InB2B { get; set; }

        public virtual bool IsRead { get; set; }

        public virtual bool IsCreate { get; set; }

        public virtual bool IsUpdate { get; set; }

        public virtual bool IsDelete { get; set; }

        public virtual bool IsFav { get; set; }
    }
}