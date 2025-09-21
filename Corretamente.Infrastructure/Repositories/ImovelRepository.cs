using Corretamente.Domain.Entities;
using Corretamente.Domain.Interfaces.Repositories;
using Corretamente.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corretamente.Infrastructure.Repositories
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly ApplicationDbContext _context;

        public ImovelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Imovel?> GetByIdAsync(int id)
        {
            return await _context.Imoveis.FindAsync(id);
        }

        public async Task<IEnumerable<Imovel>> GetAllAsync()
        {
            return await _context.Imoveis.ToListAsync();
        }

        public async Task CreateAsync(Imovel imovel)
        {
            await _context.Imoveis.AddAsync(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Imovel imovel)
        {
            _context.Imoveis.Update(imovel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
                await _context.SaveChangesAsync();
            }
        }
    }
}
