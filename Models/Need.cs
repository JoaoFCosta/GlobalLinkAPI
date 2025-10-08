using System.ComponentModel.DataAnnotations;

namespace GlobalLinkAPI.Models
{
    public class Need
    {
        [Key]
        public int NecessidadeId { get; set; }
        public string NecessidadeTitulo { get; set; }
        public string NecessidadeDescricao { get; set; }
        public string Urgencia { get; set; }
        public string Categoria { get; set; }
        public string Local { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int OngId { get; set; }
        public Ong? Ong { get; set; }
    }
}
