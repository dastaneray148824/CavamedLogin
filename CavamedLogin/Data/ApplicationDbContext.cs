using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CavamedLogin.Data;    // ApplicationUser’ýn olduðu namespace

namespace CavamedLogin.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>            // ? Burada miras (base class) belirtilir
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)                              // ? options’ý base’e ilet
        {
        }

        // Ýstersen buraya DbSet’ler ekleyebilirsin:
        // public DbSet<Something> Somethings { get; set; }
    }
}
