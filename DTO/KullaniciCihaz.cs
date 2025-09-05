
using Enums;

namespace DTO
{
    public class KullaniciCihaz : Core
    {
        public virtual Enums.DeviceType? DeviceType { get; set; }

        public virtual DeviceName? DeviceName { get; set; }
    }
}
