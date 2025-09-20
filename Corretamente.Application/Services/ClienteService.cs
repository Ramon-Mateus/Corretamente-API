using AutoMapper;
using Corretamente.Application.DTOs.Cliente;
using Corretamente.Application.Interfaces.Services;
using Corretamente.Domain.Entities;
using Corretamente.Domain.Interfaces.Repositories;

namespace Corretamente.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
    }

    public async Task<ClienteDTO?> GetByIdAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        
        if (cliente == null) return null;
        
        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
    {
        var clientes = await _clienteRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
    }

    public async Task<ClienteDTO> CreateAsync(CreateClienteDTO clienteDto)
    {
        if(!string.IsNullOrEmpty(clienteDto.Email))
        {
            var existEmailClient = await _clienteRepository.ExistsByEmailAsync(clienteDto.Email);
            if (existEmailClient)
                throw new InvalidOperationException("Já existe um cliente com esse e-mail.");
        }

        var existDocumentoClient = await _clienteRepository.ExistsByDocumentoAsync(clienteDto.Documento);
        if (existDocumentoClient)
            throw new InvalidOperationException("Já existe um cliente com esse documento.");

        var cliente = _mapper.Map<Cliente>(clienteDto);
        await _clienteRepository.AddAsync(cliente);
        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<ClienteDTO?> UpdateAsync(int id, UpdateClienteDTO clienteDto)
    {
        var existingCliente = await _clienteRepository.GetByIdAsync(id);
        
        if (existingCliente == null) return null;

        _mapper.Map(clienteDto, existingCliente);
        await _clienteRepository.UpdateAsync(existingCliente);
        
        return _mapper.Map<ClienteDTO>(existingCliente);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cliente = await _clienteRepository.GetByIdAsync(id);
        
        if (cliente == null) return false;

        await _clienteRepository.DeleteAsync(id);
        return true;
    }
}