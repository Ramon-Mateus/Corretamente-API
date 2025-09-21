using Corretamente.Application.DTOs.Imovel;
using FluentValidation;

namespace Corretamente.Application.Validators.Imovel
{
    public class UpdateImovelDtoValidator : AbstractValidator<UpdateImovelDTO>
    {
        public UpdateImovelDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("O valor deve ser maior que zero.");

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("O logradouro é obrigatório.")
                .MaximumLength(150).WithMessage("O logradouro deve ter no máximo 150 caracteres.");

            RuleFor(x => x.Numero)
                .MaximumLength(20).WithMessage("O número deve ter no máximo 20 caracteres.");

            RuleFor(x => x.Bairro)
                .NotEmpty().WithMessage("O bairro é obrigatório.")
                .MaximumLength(100).WithMessage("O bairro deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("A cidade é obrigatória.")
                .MaximumLength(100).WithMessage("A cidade deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("O estado é obrigatório.")
                .Length(2).WithMessage("O estado deve ter 2 caracteres (UF).");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("O CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("Formato de CEP inválido. Use XXXXX-XXX.");
        }
    }
}