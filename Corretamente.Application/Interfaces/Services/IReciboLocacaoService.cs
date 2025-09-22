namespace Corretamente.Application.Interfaces.Services
{
    public interface IReciboLocacaoService
    {
        Task<byte[]> GerarReciboPdfAsync(int imovelId);
    }
}
