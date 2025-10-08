namespace Corretamente.Domain.Entities;

public class Imovel
{
    public int Id { get; private set; }
    public double Valor { get; private set; }
    public string Logradouro { get; private set; } = string.Empty;
    public string Numero { get; private set; } = string.Empty;
    public string Bairro { get; private set; } = string.Empty;
    public string Cidade { get; private set; } = string.Empty;
    public string Estado { get; private set; } = string.Empty;
    public string Cep { get; private set; } = string.Empty;

    public int ProprietarioId { get; private set; }
    public Cliente Proprietario { get; private set; } = null!;
    public int? LocatarioId { get; private set; }
    public Cliente? Locatario { get; private set; }
}