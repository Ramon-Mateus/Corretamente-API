using Corretamente.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Corretamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosLocacaoController : ControllerBase
    {
        private readonly IReciboLocacaoService _reciboLocacaoService;

        // Injetamos a interface, não a implementação concreta
        public RecibosLocacaoController(IReciboLocacaoService reciboLocacaoService)
        {
            _reciboLocacaoService = reciboLocacaoService;
        }

        [HttpGet("gerar/{imovelId}")]
        public async Task<IActionResult> GerarRecibo(int imovelId)
        {
            // 3. DELEGA toda a lógica para o serviço da camada de aplicação
            byte[] pdfBytes = await _reciboLocacaoService.GerarReciboPdfAsync(imovelId);

            // 4. Se tudo deu certo, retorna o arquivo PDF
            return File(pdfBytes, "application/pdf", $"recibo_imovel_{imovelId}_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}
