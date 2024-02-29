using Organizer.Application.Common.Interfaces;

namespace Organizer.Application.FinancialService.Commands.DeleteEpenseForBalance;

public record DeleteEpenseForBalanceCommand(int Id) : IRequest<object>
{
}

public class DeleteEpenseForBalanceCommandValidator : AbstractValidator<DeleteEpenseForBalanceCommand>
{
    public DeleteEpenseForBalanceCommandValidator()
    {
    }
}

public class DeleteEpenseForBalanceCommandHandler : IRequestHandler<DeleteEpenseForBalanceCommand, object>
{
    private readonly IApplicationDbContext _context;

    public DeleteEpenseForBalanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> Handle(DeleteEpenseForBalanceCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
