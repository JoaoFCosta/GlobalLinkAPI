using System.ComponentModel.DataAnnotations;

namespace GlobalLinkAPI.Models
{
    public enum Categoria
    {
        Alimentação,
        Vestuário,
        Higiene,
        Educação,
        Saúde
    }

    public enum Urgencia
    {
        Baixa,
        Média,
        Alta
    }

    public class Need
    {
        [Key]
        public int NecessidadeId { get; set; }
        public string NecessidadeTitulo { get; set; }
        public string NecessidadeDescricao { get; set; }

        [EnumDataType(typeof(Categoria))]
        public Categoria NecessidadeCategoria { get; set; }

        [EnumDataType(typeof(Urgencia))]
        public Urgencia NecessidadeUrgencia { get; set; }

        public string Local { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public int OngId { get; set; }
        public Ong? Ong { get; set; }
    }
}
