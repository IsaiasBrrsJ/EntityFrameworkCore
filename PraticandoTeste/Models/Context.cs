using Microsoft.EntityFrameworkCore;
using PraticandoTeste.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticandoTeste.Models
{
    public class Context : DbContext
    {
        public Context() { }

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Cliente> Clientes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string con = "Server=localhost;Database=meuBancoTeeeste;Trusted_Connection=True;TrustServerCertificate=True;language=portuguese;";

            optionsBuilder.UseSqlServer(con);
        }
    }
}
