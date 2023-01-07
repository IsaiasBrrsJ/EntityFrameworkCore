using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticandoTeste.Models.Entities
{
    public class Livro
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public int QuantidadeDePaginas { get; set; }

        public Autor Autor { get; set; } = null!;
        public int AutorId { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; } = null!;
        

    }
}
