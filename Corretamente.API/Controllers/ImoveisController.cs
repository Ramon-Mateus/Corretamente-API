using Corretamente.Application.DTOs.Imovel;
using Corretamente.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corretamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImoveisController : ControllerBase
    {
        private readonly IImovelService _imovelService;

        public ImoveisController(IImovelService imovelService)
        {
            _imovelService = imovelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImovelDTO>>> GetAll()
        {
            var imoveis = await _imovelService.GetAllAsync();
            return Ok(imoveis);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImovelDTO>> GetById(int id)
        {
            var imovel = await _imovelService.GetByIdAsync(id);
            if (imovel == null) throw new KeyNotFoundException($"Imóvel com ID {id} não foi encontrado.");
            return Ok(imovel);
        }

        [HttpPost]
        public async Task<ActionResult<ImovelDTO>> Create(CreateImovelDTO imovelDto)
        {
            var createdImovel = await _imovelService.CreateAsync(imovelDto);
            return CreatedAtAction(nameof(GetById), new { id = createdImovel.Id }, createdImovel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ImovelDTO>> Update(int id, UpdateImovelDTO imovelDto)
        {
            var updatedImovel = await _imovelService.UpdateAsync(id, imovelDto);
            if (updatedImovel == null) throw new KeyNotFoundException($"Imóvel com ID {id} não foi encontrado.");
            return Ok(updatedImovel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _imovelService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
