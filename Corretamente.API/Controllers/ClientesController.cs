using Corretamente.Application.DTOs.Cliente;
using Corretamente.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corretamente.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
    {
        var clientes = await _clienteService.GetAllAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteDTO>> GetById(int id)
    {
        var cliente = await _clienteService.GetByIdAsync(id);
        if (cliente == null) throw new KeyNotFoundException($"Cliente com ID {id} não foi encontrado.");
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> Create(CreateClienteDTO clienteDto)
    {
        var createdCliente = await _clienteService.CreateAsync(clienteDto);
        return CreatedAtAction(nameof(GetById), new { id = createdCliente.Id }, createdCliente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClienteDTO>> Update(int id, UpdateClienteDTO clienteDto)
    {
        var updatedCliente = await _clienteService.UpdateAsync(id, clienteDto);
        if (updatedCliente == null) throw new KeyNotFoundException($"Cliente com ID {id} não foi encontrado.");
        return Ok(updatedCliente);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _clienteService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}