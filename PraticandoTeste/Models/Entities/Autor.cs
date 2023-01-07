using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraticandoTeste.Models.Entities
{
    public class Autor
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public virtual ICollection<Livro> Livros { get; set; } = null!;
    }
}
