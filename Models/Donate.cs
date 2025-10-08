using System.ComponentModel.DataAnnotations;

namespace GlobalLinkAPI.Models
{
    public class Donate
    {
        public int Id { get; set; }

        // Quem doou (empresa)
        public int EmpresaId { get; set; }
        public Company? Company { get; set; }

        // ONG beneficiada
        public int OngId { get; set; }
        public Ong? Ong { get; set; }

        // Necessidade atendida (opcional, pois pode ser doação espontânea)
        public int? NecessidadeId { get; set; }
        public Need? Need { get; set; }

        // Tipo de doação (ex.: "Financeira", "Alimentos", "Roupas")
        public string Tipo { get; set; }

        // Valor em dinheiro (se for financeira)
        public float? Valor { get; set; }

        // Quantidade de itens (se for material)
        public int? Quantidade { get; set; }

        // Status
        public string Status { get; set; }

        // Observações adicionais
        public string? Observacoes { get; set; }

        // Datas
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataRecebimento { get; set; }
    }
}
