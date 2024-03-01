using Organizer.Application.Common.Exceptions;
using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Commands.InviteFeminist;

public record InviteFeministCommand(string Email, int CollectiveId) : IRequest
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

public class InviteFeministCommandHandler : IRequestHandler<InviteFeministCommand>
{
    private readonly IApplicationDbContext _context;

    public InviteFeministCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(InviteFeministCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Feminists.Where(x => x.Email == request.Email).First();
        if (user == null) { throw new UserMustRegisterFIrstException(); }
        var collective = _context.Collectives.Where(x => x.Id == request.CollectiveId).First();

        var FeministCollectives = new FeministCollective
        {
            Balance = 0,
            Feminist = user,
            Collective = collective
        };
        _context.FeministCollectives.Add(FeministCollectives);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
