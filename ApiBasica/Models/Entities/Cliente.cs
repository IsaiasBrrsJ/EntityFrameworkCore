namespace ApiBasica.Models.Entities
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; }

        public Veiculo Veiculo { get; set; }    
        public Cliente() { }
   }
}
