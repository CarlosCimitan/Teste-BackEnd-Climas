namespace Clima.Models
{
    public class ClimaModel
    {
        public int id { get; set; }
        public string Cidade { get; set; }
        public double Temperatura { get; set; }
        public string CondicaoTempo { get; set; }
        public DateTime Date { get; set; }
    }
}
