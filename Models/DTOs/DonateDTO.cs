namespace GlobalLinkAPI;

public class DonateDTO
{
    public int Id { get; set; }             // Id da doação
    public int EmpresaId { get; set; }      // Id da empresa/doador
    public int OngId { get; set; }          // Id da ONG
    public string Tipo { get; set; } = "";  // Tipo da doação
    public string? Observacoes { get; set; }
    public string Status { get; set; } = "Pendente"; // Status da doação

    // Campos para retornar nomes no GET/POST
    public string? EmpresaNome { get; set; }
    public string? OngNome { get; set; }
}
