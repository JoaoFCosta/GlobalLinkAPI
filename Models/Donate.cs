using System;
using System.ComponentModel.DataAnnotations;

namespace GlobalLinkAPI.Models
{
    public class Donate
    {
        [Key]
        public int Id { get; set; }

        // Quem doou (empresa)
        [Required]
        public int EmpresaId { get; set; }

        public Company? Empresa { get; set; }

        // ONG beneficiada
        [Required]
        public int OngId { get; set; }

        public Ong? Ong { get; set; }

        // Necessidade atendida (opcional, pois pode ser doação espontânea)
        public int? NecessidadeId { get; set; }

        public Need? Need { get; set; }

        // Tipo de doação (ex.: "Financeira", "Alimentos", "Roupas")
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; } = string.Empty;

        // Valor em dinheiro (se for financeira)
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public float? Valor { get; set; }

        // Quantidade de itens (se for material)
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser positiva.")]
        public int? Quantidade { get; set; }

        // Status da doação (ex.: "Pendente", "Recebida", "Cancelada")
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pendente";

        // Observações adicionais
        [MaxLength(500)]
        public string? Observacoes { get; set; }

        // Datas
        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataRecebimento { get; set; }
    }
}
