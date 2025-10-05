using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GlobalLinkAPI.Data;
using GlobalLinkAPI.Models;
using GlobalLinkAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace GlobalLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly GlobalLinkDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(GlobalLinkDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ---------------- MÉTODO CENTRAL DE TOKEN ----------------
        private string GenerateJwtToken(string id, string name, string email, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // ---------------- REGISTRO EMPRESA ----------------
        [HttpPost("CompanyRegister")]
        public async Task<ActionResult> CompanyRegister([FromBody] RegisterRequest request)
        {
            if (_context.Companies.Any(c => c.EmpresaEmail == request.Email))
                return BadRequest("Email já cadastrado.");

            var company = new Company
            {
                EmpresaNome = request.Nome,
                EmpresaEmail = request.Email,
                EmpresaCnpj = request.Cnpj,
                EmpresaTelefone = request.Telefone,
                EmpresaRua = request.Rua,
                EmpresaNumero = request.Numero,
                EmpresaBairro = request.Bairro,
                EmpresaCep = request.Cep,
                EmpresaSenha = ComputeSha256Hash(request.Senha)
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cadastro realizado com sucesso", companyId = company.EmpresaId });
        }

        // ---------------- REGISTRO ONG ----------------
        [HttpPost("OngRegister")]
        public async Task<ActionResult> OngRegister([FromBody] RegisterRequest request)
        {
            if (_context.Ongs.Any(o => o.OngEmail == request.Email))
                return BadRequest("Email já cadastrado.");

            var ong = new Ong
            {
                OngNome = request.Nome,
                OngEmail = request.Email,
                OngCnpj = request.Cnpj,
                OngTelefone = request.Telefone,
                OngRua = request.Rua,
                OngNumero = request.Numero,
                OngBairro = request.Bairro,
                OngCep = request.Cep,
                OngSenha = ComputeSha256Hash(request.Senha)
            };

            _context.Ongs.Add(ong);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cadastro realizado com sucesso", ongId = ong.OngId });
        }

        // ---------------- LOGIN EMPRESA ----------------
        [HttpPost("CompanyLogin")]
        public async Task<ActionResult> CompanyLogin([FromBody] LoginRequest request)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.EmpresaEmail == request.Email);
            if (company == null)
                return Unauthorized("Email não encontrado.");

            var hash = ComputeSha256Hash(request.Senha);
            if (company.EmpresaSenha != hash)
                return Unauthorized("Senha incorreta.");

            var token = GenerateJwtToken(
                company.EmpresaId.ToString(),
                company.EmpresaNome,
                company.EmpresaEmail,
                "Company"
            );

            return Ok(new { message = "Login realizado com sucesso", token });
        }

        // ---------------- LOGIN ONG ----------------
        [HttpPost("OngLogin")]
        public async Task<ActionResult> OngLogin([FromBody] LoginRequest request)
        {
            var ong = await _context.Ongs.FirstOrDefaultAsync(o => o.OngEmail == request.Email);
            if (ong == null)
                return Unauthorized("Email não encontrado.");

            var hash = ComputeSha256Hash(request.Senha);
            if (ong.OngSenha != hash)
                return Unauthorized("Senha incorreta.");

            var token = GenerateJwtToken(
                ong.OngId.ToString(),
                ong.OngNome,
                ong.OngEmail,
                "Ong"
            );

            return Ok(new { message = "Login realizado com sucesso", token });
        }

        // ---------------- HELPER: HASH SENHA ----------------
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
