using Corretamente.Application.DTOs.Cliente;

namespace Corretamente.Application.Interfaces.Services;

public interface IClienteService
{
    Task<ClienteDTO?> GetByIdAsync(int id);
    Task<IEnumerable<ClienteDTO>> GetAllAsync();
    Task<ClienteDTO> CreateAsync(CreateClienteDTO clienteDto);
    Task<ClienteDTO?> UpdateAsync(int id, UpdateClienteDTO clienteDto);
    Task<bool> DeleteAsync(int id);
}