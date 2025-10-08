namespace Corretamente.Application.DTOs.Imovel
{
    public class UpdateImovelDTO
    {
        public double Valor { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public int? LocatarioId { get; set; }
        public int ProprietarioId { get; set; }
    }
}
