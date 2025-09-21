using Corretamente.Application.DTOs.Cliente;
using FluentValidation;

namespace Corretamente.Application.Validators.Cliente
{
    public class UpdateClienteDtoValidator : AbstractValidator<UpdateClienteDTO>
    {
        public UpdateClienteDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter pelo menos 3 caracteres")
                .MaximumLength(100).WithMessage("Nome não pode exceder 100 caracteres");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email inválido")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.Telefone)
                .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$")
                .WithMessage("Telefone inválido")
                .When(x => !string.IsNullOrEmpty(x.Telefone));

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("Documento é obrigatório")
                .Must(BeValidDocument).WithMessage("Documento inválido");

            RuleFor(x => x.IsPessoaJuridica)
                .NotNull().WithMessage("Tipo de pessoa é obrigatório");
        }

        private bool BeValidDocument(string documento)
        {
            // Validação de CPF/CNPJ
            if (string.IsNullOrEmpty(documento)) return false;

            var cleanDoc = new string(documento.Where(char.IsDigit).ToArray());

            if (cleanDoc.Length == 11) // CPF
            {
                return ValidarCPF(cleanDoc);
            }
            else if (cleanDoc.Length == 14) // CNPJ
            {
                return ValidarCNPJ(cleanDoc);
            }
            return false;
        }

        private bool ValidarCPF(string cpf)
        {
            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica se tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(c => c == cpf[0]))
                return false;

            // Cálculo do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            // Verifica primeiro dígito
            if (int.Parse(cpf[9].ToString()) != digito1)
                return false;

            // Cálculo do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Verifica segundo dígito
            return int.Parse(cpf[10].ToString()) == digito2;
        }

        private bool ValidarCNPJ(string cnpj)
        {
            // Remove caracteres não numéricos
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            // Verifica se tem 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.All(c => c == cnpj[0]))
                return false;

            // Cálculo do primeiro dígito verificador
            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            // Verifica primeiro dígito
            if (int.Parse(cnpj[12].ToString()) != digito1)
                return false;

            // Cálculo do segundo dígito verificador
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(cnpj[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Verifica segundo dígito
            return int.Parse(cnpj[13].ToString()) == digito2;
        }
    }
}
