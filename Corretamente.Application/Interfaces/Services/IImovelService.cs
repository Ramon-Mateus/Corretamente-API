using Corretamente.Application.DTOs.Cliente;
using Corretamente.Application.DTOs.Imovel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corretamente.Application.Interfaces.Services
{
    public interface IImovelService
    {
        Task<ImovelDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ImovelDTO>> GetAllAsync();
        Task<ImovelDTO> CreateAsync(CreateImovelDTO imovelDto);
        Task<ImovelDTO?> UpdateAsync(int id, UpdateImovelDTO imovelDto);
        Task<bool> DeleteAsync(int id);
    }
}
