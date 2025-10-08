using Corretamente.Application.DTOs.Documentos;
using Corretamente.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace Corretamente.Infrastructure.Pdf
{
    public class QuestPdfReciboGenerator : IReciboLocacaoGenerator
    {
        public QuestPdfReciboGenerator()
        {
            // IMPORTANTE: Configurar a licença para uso em produção
            // A licença Community é gratuita para projetos open-source e para empresas com receita < $1M
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] Generate(ReciboLocacaoDTO data)
        {
            // Gera o documento usando a classe de layout e retorna os bytes
            var document = new ReciboDocument(data);
            return document.GeneratePdf();
        }
    }

    internal class ReciboDocument : IDocument
    {
        private readonly ReciboLocacaoDTO _data;

        public ReciboDocument(ReciboLocacaoDTO data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            var PagadorCpfOrCnpjLabel = _data.PagadorIsPessoaJuridica ? "CNPJ" : "CPF";
            var EmitenteCpfOrCnpjLabel = _data.EmitenteIsPessoaJuridica ? "CNPJ" : "CPF";

            container.Page(page =>
            {
                // Configurações da página
                page.Margin(50);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Arial"));

                // Conteúdo principal em uma coluna
                page.Content().Column(column =>
                {
                    // Espaçamento entre os itens da coluna
                    column.Spacing(30);

                    // --- CABEÇALHO ---
                    column.Item().Row(row =>
                    {
                        row.RelativeItem().Text("RECIBO LOCAÇÃO")
                            .SemiBold().FontSize(16);

                        row.RelativeItem().AlignRight().Text($"R$ {_data.Valor:N2}")
                            .SemiBold().FontSize(16);
                    });

                    // --- CORPO DO TEXTO ---
                    column.Item().PaddingTop(1, Unit.Inch).Text(text =>
                    {
                        text.DefaultTextStyle(x => x.LineHeight(1.5f));
                        text.Justify();

                        text.Span("Recebi do Sr. ");
                        text.Span($"{_data.PagadorNome}, ").Bold();
                        text.Span("INSCRITO NO ");
                        text.Span($"{PagadorCpfOrCnpjLabel} Nº {_data.PagadorDocumento}, ").Bold();
                        text.Span($"o valor de R$ {_data.Valor:N2} ({_data.ValorPorExtenso}), ");
                        text.Span($"referente à {_data.ReferenteA}, ");
                        text.Span($"com vencimento em {_data.DataVencimento.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("pt-BR"))}, ");
                        text.Span("pelo que assino o presente recibo, dando plena e total quitação.");
                    });

                    // --- DATA DE EMISSÃO ---
                    var ptBr = new CultureInfo("pt-BR");
                    string dataEmissaoFormatada = $"{_data.Local}, {_data.DataEmissao.ToString("dd 'de' MMMM 'de' yyyy", ptBr)}.";

                    column.Item().PaddingTop(2, Unit.Inch).AlignRight().Text(dataEmissaoFormatada);

                    // --- ÁREA DE ASSINATURA ---
                    column.Item().PaddingTop(2, Unit.Inch).AlignCenter().Column(signatureColumn =>
                    {
                        signatureColumn.Spacing(5);

                        // Linha da assinatura
                        signatureColumn.Item().Width(250, Unit.Point).LineHorizontal(1);

                        // Nome do emitente
                        signatureColumn.Item().Text(_data.EmitenteNome).SemiBold();

                        // CNPJ do emitente
                        signatureColumn.Item().Text($"{EmitenteCpfOrCnpjLabel} {_data.EmitenteDocumento}");
                    });
                });
            });
        }
    }
}
