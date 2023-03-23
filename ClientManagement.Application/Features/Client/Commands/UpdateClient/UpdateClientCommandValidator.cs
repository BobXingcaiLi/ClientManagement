using ClientManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace ClientManagement.Application.Features.Client.Commands.UpdateClient;

public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public UpdateClientCommandValidator(IClientRepository clientRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(ClientMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} is required");

        _clientRepository = clientRepository;
    }

    private async Task<bool> ClientMustExist(int id, CancellationToken token)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client != null;
    }
}
