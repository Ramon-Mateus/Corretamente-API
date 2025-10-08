using Corretamente.Application.DTOs.Cliente;

namespace Corretamente.Application.DTOs.Imovel
{
    public class ImovelDTO
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public ClienteDTO? Locatario { get; set; }
        public ClienteDTO Proprietario { get; set; }
    }
}
