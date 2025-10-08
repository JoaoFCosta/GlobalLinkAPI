namespace GlobalLinkAPI;

public class DonateDTO
{
    public int Id { get; set; } // Id da doação
    public string EmpresaNome { get; set; } = string.Empty; // Nome da empresa/doador
    public string Tipo { get; set; } = string.Empty; // Tipo de doação
    public string? Observacoes { get; set; } // Observações
    public string Status { get; set; } = string.Empty; // Status da doação
}
