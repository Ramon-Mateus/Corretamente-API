using Corretamente.Application.DTOs.Documentos;

namespace Corretamente.Application.Interfaces.Services
{
    public interface IReciboLocacaoGenerator
    {
        byte[] Generate(ReciboLocacaoDTO data);
    }
}
