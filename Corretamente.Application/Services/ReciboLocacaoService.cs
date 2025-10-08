using Corretamente.Application.DTOs.Documentos;
using Corretamente.Application.Interfaces.Services;
using Corretamente.Domain.Interfaces.Repositories;
using System.Globalization;
using Humanizer;

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
                PagadorNome = imovel.Locatario!.Nome,
                PagadorDocumento = imovel.Locatario!.IsPessoaJuridica ? FormatarCnpj(imovel.Locatario!.Documento) : FormatarCpf(imovel.Locatario!.Documento),
                Valor = (decimal)imovel.Valor,
                ValorPorExtenso = ConverterValorComCentavos((decimal)imovel.Valor),
                ReferenteA = $"locação do imóvel situado na {imovel.Logradouro}, {imovel.Numero}, {imovel.Bairro} – {imovel.Cidade} / {imovel.Estado}",
                DataVencimento = new DateOnly(2025, 3, 10),
                EmitenteNome = imovel.Proprietario.Nome,
                EmitenteDocumento = imovel.Proprietario.Documento,
                Local = $"{imovel.Cidade}/{imovel.Estado}",
                DataEmissao = DateOnly.FromDateTime(DateTime.Now),
                PagadorIsPessoaJuridica = imovel.Locatario!.IsPessoaJuridica
            };

            // 3. Chamar o gerador de PDF (que está na camada de infraestrutura)
            byte[] pdfBytes = _reciboLocacaoGenerator.Generate(reciboDto);

            return pdfBytes;
        }

        private string ConverterValorComCentavos(decimal valor)
        {
            var cultura = new CultureInfo("pt-BR");

            // Separa a parte inteira dos centavos
            long parteInteira = (long)Math.Truncate(valor);
            int centavos = (int)((valor - parteInteira) * 100);

            // Converte a parte inteira
            string extensoInteiro = parteInteira.ToWords(cultura);
            string resultado = extensoInteiro + (parteInteira == 1 ? " real" : " reais");

            // Adiciona os centavos se houver
            if (centavos > 0)
            {
                string extensoCentavos = centavos.ToWords(cultura);
                resultado += " e " + extensoCentavos + (centavos == 1 ? " centavo" : " centavos");
            }

            return resultado;
        }

        public static string FormatarCnpj(string cnpj)
        {
            var numeros = new string(cnpj.Where(char.IsDigit).ToArray());

            if (numeros.Length != 14)
            {
                throw new ArgumentException("O CNPJ deve conter 14 dígitos.");
            }

            return Convert.ToUInt64(numeros).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormatarCpf(string cpf)
        {
            var numeros = new string(cpf.Where(char.IsDigit).ToArray());

            if (numeros.Length != 11)
            {
                throw new ArgumentException("O CPF deve conter 11 dígitos.");
            }

            return Convert.ToInt64(numeros).ToString(@"000\.000\.000\-00");
        }
    }
}
