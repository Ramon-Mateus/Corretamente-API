namespace Corretamente.Domain.Entities;

public class Cliente
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string? Email { get; private set; }  = string.Empty;
    public string? Telefone { get; private set; } = string.Empty;
    public string Documento  { get; private set; } = string.Empty;
    public bool IsPessoaJuridica { get; private set; } = false;

    public ICollection<Imovel> ImoveisProprios { get; private set; } = new List<Imovel>();

    public ICollection<Imovel> ImoveisAlugados { get; private set; } = new List<Imovel>();
}