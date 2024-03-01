using Organizer.Application.Common.Interfaces;

namespace Organizer.Application.FinancialService.Commands.DeleteEpenseForBalance;

public record DeleteEpenseForBalanceCommand(int Id) : IRequest
{
}

public class DeleteEpenseForBalanceCommandValidator : AbstractValidator<DeleteEpenseForBalanceCommand>
{
    public DeleteEpenseForBalanceCommandValidator()
    {
    }
}

public class DeleteEpenseForBalanceCommandHandler : IRequestHandler<DeleteEpenseForBalanceCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEpenseForBalanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteEpenseForBalanceCommand request, CancellationToken cancellationToken)
    {
        _context.Expenses.Remove(_context.Expenses.Where(x => x.Id == request.Id).First());
        await _context.SaveChangesAsync(cancellationToken);

    }
}
