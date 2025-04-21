using Csharp.Models;
using Csharp.Models;
using Microsoft.EntityFrameworkCore;

namespace Csharp.Data
{
    public class MujDbContext : DbContext
    {
        public DbSet<Uzivatel> Uzivatele { get; set; }

        public MujDbContext(DbContextOptions<MujDbContext> options) : base(options) { }
    }
}
