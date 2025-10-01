using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GlobalLinkAPI.Models
{
    public class Ong
    {
        public int OngId { get; set; }
        public string OngNome { get; set; }
        public string OngCnpj { get; set; }
        public string OngEmail { get; set; }
        public string OngTelefone { get; set; }
        public string OngSenha { get; set; }
        public string OngRua { get; set; }
        public string OngNumero { get; set; }
        public string OngBairro { get; set; }
        public string OngCep { get; set; }
        public string? OngComplemento { get; set; }
        public string OngMissao { get; set; }
    }
}
