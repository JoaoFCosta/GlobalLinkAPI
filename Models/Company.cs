using System.ComponentModel.DataAnnotations;

namespace GlobalLinkAPI.Models
{
    public class Company
    {
        [Key]
        public int EmpresaId { get; set; }
        public string EmpresaNome { get; set; }
        public string EmpresaCnpj { get; set; }
        public string EmpresaEmail { get; set; }
        public string EmpresaTelefone { get; set; }
        public string EmpresaSenha { get; set; }
        public string EmpresaRua { get; set; }
        public string EmpresaNumero { get; set; }
        public string EmpresaBairro { get; set; }
        public string EmpresaCep { get; set; }
        public string? EmpresaComplemento { get; set; }
    }
}
