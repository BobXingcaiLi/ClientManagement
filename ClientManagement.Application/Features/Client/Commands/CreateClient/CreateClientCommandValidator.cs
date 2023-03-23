using ClientManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace ClientManagement.Application.Features.Client.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandValidator(IClientRepository clientRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(70).WithMessage("{PropertyName} must be fewer than 70 characters");

        RuleFor(p => p.EmailAddress)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .EmailAddress().WithMessage("{PropertyName} is required");

        RuleFor(q => q)
            .MustAsync(ClientNameUnique)
            .WithMessage("Leave type already exists");


        _clientRepository = clientRepository;

    }

    private Task<bool> ClientNameUnique(CreateClientCommand command, CancellationToken token)
    {
        return _clientRepository.IsClientUnique(command.Name);
    }
}
