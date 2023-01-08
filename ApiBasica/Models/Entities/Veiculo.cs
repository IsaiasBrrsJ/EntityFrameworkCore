namespace ApiBasica.Models.Entities
{
    public class Veiculo
    {
        public int Id { get; set; }

        public string NomeCarro { get; set; } = string.Empty;

        public string Placa { get; set; } = string.Empty;

        public DateTime DataEntrada { get; set; }

        public DateTime DataSaida { get; set; }

        public Cliente Cliente { get; set; } 
        public int ClienteId { get; set; }
        public Veiculo() { }

    }
}
