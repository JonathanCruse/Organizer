using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Commands.DeleteCollective;

public record DeleteCollectiveCommand(int id) : IRequest
{
}

public class DeleteCollectiveCommandValidator : AbstractValidator<DeleteCollectiveCommand>
{
    public DeleteCollectiveCommandValidator()
    {
    }
}

public class DeleteCollectiveCommandHandler : IRequestHandler<DeleteCollectiveCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCollectiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCollectiveCommand request, CancellationToken cancellationToken)
    {
        Collective collective = _context.Collectives.Where(x => x.Id == request.id).First();
        foreach (var item in collective.Transactions)
        {
            _context.Expenses.RemoveRange(item.Expenses);
            _context.Transactions.Remove(item);
        }
        _context.Collectives.Remove(collective);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
