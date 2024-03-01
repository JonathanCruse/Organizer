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
            .MaximumLength(150);
    }
}

public class CreateCollectiveCommandHandler : IRequestHandler<CreateCollectiveCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public CreateCollectiveCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(CreateCollectiveCommand request, CancellationToken cancellationToken)
    {
        Collective collective = new Collective { 
            Name = request.Name,
        };
        _context.Collectives.Add(collective);

        FeministCollective feministCollective = new FeministCollective { Feminist = _context.Feminists.Where(x => x.Id == _user.Id).First(), Collective = collective, Balance = 0 };
        _context.FeministCollectives.Add(feministCollective);
        await _context.SaveChangesAsync(cancellationToken);

        return collective.Id;
    }
}
