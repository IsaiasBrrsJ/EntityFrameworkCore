using ApiBasica.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBasica.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) :base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Veiculo> Veiculos { get; set; }


        
    }
}
