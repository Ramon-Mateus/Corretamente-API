using Corretamente.Domain.Entities;

namespace Corretamente.Domain.Interfaces.Repositories
{
    public interface IImovelRepository
    {
        Task<Imovel?> GetByIdAsync(int id);
        Task<IEnumerable<Imovel>> GetAllAsync();
        Task CreateAsync(Imovel imovel);
        Task UpdateAsync(Imovel imovel);
        Task DeleteAsync(int id);
    }
}
