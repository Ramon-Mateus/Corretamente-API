using Corretamente.Application.DTOs.Documentos;
using Corretamente.Application.Interfaces.Services;
using Corretamente.Domain.Interfaces.Repositories;
using System.Globalization;

namespace Corretamente.Application.Services
{
    public class ReciboLocacaoService : IReciboLocacaoService
    {
        private readonly IImovelRepository _imovelRepository;
        private readonly IReciboLocacaoGenerator _reciboLocacaoGenerator;

        public ReciboLocacaoService(IImovelRepository imovelRepository, IReciboLocacaoGenerator reciboLocacaoGenerator)
        {
            _imovelRepository = imovelRepository;
            _reciboLocacaoGenerator = reciboLocacaoGenerator;
        }

        public async Task<byte[]> GerarReciboPdfAsync(int imovelId)
        {
            // 1. Buscar os dados do banco usando o repositório
            var imovel = await _imovelRepository.GetByIdAsync(imovelId);

            if (imovel == null)
            {
                throw new Exception($"Imóvel com ID {imovelId} não encontrado.");
            }

            // 2. Mapear as entidades do domínio para o DTO que o gerador de PDF espera
            var reciboDto = new ReciboLocacaoDTO
            {
                PagadorNome = "VICTOR C. DE MEDEIROS",
                PagadorCnpj = "39.405.751/0001-23",
                Valor = 2750.00m,
                ValorPorExtenso = "dois mil, setecentos e cinquenta reais",
                ReferenteA = "locação do imóvel comercial situado na Rua Érico verissimo, 3389, Candelária – Natal / RN",
                DataVencimento = new DateOnly(2025, 3, 10),
                EmitenteNome = "Panificadora São João Ltda me",
                EmitenteCnpj = "03.077.549/0001-72",
                Local = "Natal/RN",
                DataEmissao = new DateOnly(2025, 3, 12)
            };

            // 3. Chamar o gerador de PDF (que está na camada de infraestrutura)
            byte[] pdfBytes = _reciboLocacaoGenerator.Generate(reciboDto);

            return pdfBytes;
        }
    }
}
