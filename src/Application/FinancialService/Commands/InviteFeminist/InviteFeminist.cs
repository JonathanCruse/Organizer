using Organizer.Application.Common.Interfaces;

namespace Organizer.Application.FinancialService.Commands.InviteFeminist;

public record InviteFeministCommand(string Email, int CollectiveId) : IRequest<int>
{
}

public class InviteFeministCommandValidator : AbstractValidator<InviteFeministCommand>
{
    public InviteFeministCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .WithMessage("must be email format");
    }
}

public class InviteFeministCommandHandler : IRequestHandler<InviteFeministCommand, int>
{
    private readonly IApplicationDbContext _context;

    public InviteFeministCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(InviteFeministCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
