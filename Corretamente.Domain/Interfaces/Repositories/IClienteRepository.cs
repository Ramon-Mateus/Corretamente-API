using Corretamente.Domain.Entities;

namespace Corretamente.Domain.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(int id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task CreateAsync(Cliente cliente);
    Task UpdateAsync(Cliente cliente);
    Task DeleteAsync(int id);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByDocumentoAsync(string doc);
}