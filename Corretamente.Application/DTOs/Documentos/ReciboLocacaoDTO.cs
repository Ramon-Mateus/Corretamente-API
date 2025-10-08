namespace Corretamente.Application.DTOs.Documentos
{
    public class ReciboLocacaoDTO
    {
        public string PagadorNome { get; set; } = string.Empty;
        public string PagadorDocumento { get; set; } = string.Empty;
        public bool PagadorIsPessoaJuridica { get; set; }
        public decimal Valor { get; set; }
        public string ValorPorExtenso { get; set; } = string.Empty;
        public string ReferenteA { get; set; } = string.Empty;
        public DateOnly DataVencimento { get; set; }
        public string EmitenteNome { get; set; } = string.Empty;
        public string EmitenteDocumento { get; set; } = string.Empty;
        public bool EmitenteIsPessoaJuridica { get; set; }
        public string Local { get; set; } = string.Empty;
        public DateOnly DataEmissao { get; set; }
    }
}
