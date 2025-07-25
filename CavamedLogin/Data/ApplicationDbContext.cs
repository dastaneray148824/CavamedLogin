using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CavamedLogin.Data;    // ApplicationUser��n oldu�u namespace

namespace CavamedLogin.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>            // ? Burada miras (base class) belirtilir
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)                              // ? options�� base�e ilet
        {
        }

        // �stersen buraya DbSet�ler ekleyebilirsin:
        // public DbSet<Something> Somethings { get; set; }
    }
}
