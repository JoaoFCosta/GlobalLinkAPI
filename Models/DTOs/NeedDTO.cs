namespace GlobalLinkAPI;

public class NeedDTO
{
    public int NecessidadeId { get; set; }           // Id da necessidade
    public string NecessidadeTitulo { get; set; } = string.Empty; // Título da necessidade
    public string NecessidadeDescricao { get; set; } = string.Empty; // Descrição
    public string Urgencia { get; set; } = string.Empty; // Urgência (ex.: Crítico, Atenção)
    public string Categoria { get; set; } = string.Empty; // Categoria (ex.: Alimento, Roupas)
    public string Local { get; set; } = string.Empty; // Local da necessidade
    public DateTime DataCriacao { get; set; } = DateTime.Now; // Data de criação
    public int OngId { get; set; } // Id da ONG
    public string? OngNome { get; set; }
}
