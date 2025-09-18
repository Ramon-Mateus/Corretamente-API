namespace Corretamente.Application.DTOs.Cliente;

public class UpdateClienteDTO
{
    public string Nome { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string Documento { get; set; } = string.Empty;
    public bool IsPessoaJuridica { get; set; }
}