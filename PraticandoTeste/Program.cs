using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PraticandoTeste.Models;
using PraticandoTeste.Models.Entities;
using System.Text.Json;

namespace PraticandoTeste
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var banco = new Context())
                {

                    // AluguelLivro(2, banco);

                    ConsultarLivrosAlugados(banco, 1);
                }


                //Console.WriteLine("Adcionado com sucesso");
              
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        static void ConsultarLivrosAlugados(Context banco, int idCliente)
        {
            var cliente = banco.Clientes.Where(x => x.Id == idCliente).Include(x => x.Livros).AsNoTracking().ToList();

            var livroAlugado =
                (
                    from clientes in banco.Clientes
                    join livros in banco.Livros
                    on clientes.Id equals idCliente
                    select new { clientes, livros }

                ).AsNoTracking().ToList();

            var serializarJson = JsonConvert.SerializeObject(livroAlugado, Formatting.Indented);

            Console.WriteLine(serializarJson);
        }
        static void AluguelLivro(int idLivro, Context banco)
        {
           var livro = banco.Livros.FirstOrDefault(x => x.Id.Equals(idLivro));
            var cliente = banco.Clientes.FirstOrDefault(x => x.Id.Equals(18));
            if (livro is null || cliente is null)
                return;

            cliente.Livros = new List<Livro>() { livro };

            banco.Update(cliente);
            banco.SaveChanges();
        }
        static Cliente AdcionarCliente(string nome)
        {
            var cliente = new Cliente();
            cliente.Nome = nome;

            return cliente;
        }
        static Livro AdcionarLivro(string nomeLivro, int quantidadePaginas, Autor autor)
        { 
            var livro = new Livro();
            livro.Nome= nomeLivro;
            livro.QuantidadeDePaginas = quantidadePaginas;
            livro.Autor = autor;


            return livro;
        }
        static Autor AdcionarAutor(string nome)
        {
            var autor = new Autor();
            autor.Nome = nome;

            return autor;
        }
    }
}