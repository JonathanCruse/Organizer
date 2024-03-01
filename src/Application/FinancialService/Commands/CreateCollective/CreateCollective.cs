using Organizer.Application.Common.Interfaces;
using FluentValidation.Validators;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Commands.CreateCollective;

public record CreateCollectiveCommand(string Name) : IRequest<int>
{
}

public class CreateCollectiveCommandValidator : AbstractValidator<CreateCollectiveCommand>
{
    public CreateCollectiveCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MinimumLength(150)
            .WithMessage("Name must contain 3-150 letters");
    }
}

public class CreateCollectiveCommandHandler : IRequestHandler<CreateCollectiveCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCollectiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCollectiveCommand request, CancellationToken cancellationToken)
    {
        Collective collective = new Collective { 
            Name = request.Name,
        };
        _context.Collectives.Add(collective);
        await _context.SaveChangesAsync(cancellationToken);

        return collective.Id;
    }
}
