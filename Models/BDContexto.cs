using Microsoft.EntityFrameworkCore;
 
namespace conexaoPostgre.Models
{
    public class BDContexto : DbContext
    {
        public BDContexto(DbContextOptions<BDContexto> options) : base(options)
        {
        }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
    }
}