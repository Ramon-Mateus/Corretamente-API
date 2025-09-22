namespace Corretamente.Application.DTOs.Documentos
{
    public class ReciboLocacaoDTO
    {
        public string PagadorNome { get; set; }
        public string PagadorCnpj { get; set; }
        public decimal Valor { get; set; }
        public string ValorPorExtenso { get; set; }
        public string ReferenteA { get; set; }
        public DateOnly DataVencimento { get; set; }
        public string EmitenteNome { get; set; }
        public string EmitenteCnpj { get; set; }
        public string Local { get; set; }
        public DateOnly DataEmissao { get; set; }
    }
}
