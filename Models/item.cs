namespace ApiAchadosEPerdidos.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public string Local { get; set; }

        public string Tipo { get; set; } // Perdido ou Encontrado

        public DateTime Data { get; set; }
    }
}