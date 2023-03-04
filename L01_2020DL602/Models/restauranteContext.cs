using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace L01_2020DL602.Models
{
    public class restauranteContext : DbContext
    {

        public restauranteContext(DbContextOptions<restauranteContext> options) : base(options) { }

        public DbSet<platos> platos { get; set; }
        public DbSet<pedidos> pedidos { get; set;}
        public DbSet<motoristas> motoristas { get; set; }

    }
}
