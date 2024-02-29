using Organizer.Application.Common.Interfaces;

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
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
