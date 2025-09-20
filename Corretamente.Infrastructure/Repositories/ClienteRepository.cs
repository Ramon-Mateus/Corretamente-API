using Corretamente.Application.DTOs.Cliente;
using Corretamente.Domain.Entities;
using Corretamente.Domain.Interfaces.Repositories;
using Corretamente.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Corretamente.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task AddAsync(Cliente cliente)
    {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Cliente cliente)
    {
        _context.Clientes.Update(cliente);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente != null)
        {
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var exists = await _context.Clientes.AnyAsync(c => c.Email == email);
        return exists;
    }

    public async Task<bool> ExistsByDocumentoAsync(string doc)
    {
        var exists = await _context.Clientes.AnyAsync(c => c.Documento == doc);
        return exists;
    }
}