
namespace Enums
{
    [Flags]
    public enum CRUD
    {
        Create = 0x1,
        Read = 0x2,
        Update = 0x4,
        Delete = 0x8
    }
}
