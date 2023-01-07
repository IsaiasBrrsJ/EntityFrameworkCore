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
                    Menu(banco);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
        static void Menu(Context banco)
        {
            while (true)
            {
                banco.SaveChanges();
                Console.Clear();
                Console.WriteLine("1-Cadastrar Autor\n2-Cadastrar Livro\n3-Cadastrar Cliente\n4-Emprestar Livro\n0-Sair\nOpcao: ");
                var opc = Console.ReadLine();

                switch (opc)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        AdcionarAutor(banco);
                        break;
                    case "2":
                        AdcionarLivro(banco);
                        break;
                        case "3":
                        AdcionarCliente(banco);
                        break;
                    case "4":
                        AluguelLivro(banco);
                        break;
                    default:
                        Console.WriteLine("Opcao Incorreta");
                        break;
                }
                Console.ReadKey();

            }
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
        static void AluguelLivro(Context banco)
        {
            Console.WriteLine("Informe o Id do Cliente: ");
            var cliente = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o Id do livro: ");
            var idLivro = int.Parse(Console.ReadLine());

            var clienteBanco = banco.Clientes.FirstOrDefault(x => x.Id == cliente);
            var livroBanco = banco.Livros.FirstOrDefault(x => x.Id == idLivro);

            Console.WriteLine(clienteBanco.Nome);
            Console.WriteLine(livroBanco.Nome);
            Console.WriteLine("Confirma, enter para continuar");
            Console.ReadKey();

            clienteBanco.Livros = new List<Livro>() { livroBanco };

            banco.Update(clienteBanco);
            Console.WriteLine("Alugado com sucesso");
        }
        static void AdcionarCliente(Context banco)
        {
            Console.WriteLine("Nome Cliente: ");
            string nomeCliente = Console.ReadLine();

            banco.Clientes.Add(new Cliente { Nome = nomeCliente});
            Console.WriteLine("Cadastrado com sucesso");
        }
        static void AdcionarLivro(Context banco)
        {
            Console.WriteLine("Nome Livro: ");
            string nomeLivro = Console.ReadLine();
            Console.WriteLine("Quantidade de páginas: ");
            int qtdPaginas = int.Parse(Console.ReadLine());
            Console.WriteLine("------------------");
            foreach (var item in banco.Autores)
            {
                Console.WriteLine(item.Id + " " + item.Nome);
            }
            Console.WriteLine("------------------");
            Console.WriteLine("Informe o Id do autor: ");
            int idAutor = int.Parse(Console.ReadLine());
            var autor = banco.Autores.FirstOrDefault(x => x.Id == idAutor);

            if (autor is null)
                return;

            banco.Livros.Add(new Livro
            {
                Nome = nomeLivro,
                QuantidadeDePaginas = qtdPaginas,
                Autor = autor
            });
            Console.WriteLine("Cadastrado com sucesso");
        }
        static void AdcionarAutor(Context banco)
        {
            Console.WriteLine("Nome Autor: ");
            string nome = Console.ReadLine();
            banco.Autores.Add(new Autor { Nome = nome});
            Console.WriteLine("Cadastrado com sucesso");
        }
    }
}