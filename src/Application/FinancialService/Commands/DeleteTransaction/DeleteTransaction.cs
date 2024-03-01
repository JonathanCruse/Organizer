using Microsoft.EntityFrameworkCore;
using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Commands.DeleteTransaction;

public record DeleteTransactionCommand(int Id) : IRequest
{
}

public class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
{
    public DeleteTransactionCommandValidator()
    {
    }
}

public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTransactionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = _context.Transactions.Where(x => x.Id == request.Id).First();
        _context.Expenses.RemoveRange(transaction.Expenses);
        _context.Transactions.Remove(transaction);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
