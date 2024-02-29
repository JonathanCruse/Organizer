using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
namespace Organizer.Application.FinancialService.Queries.GetTransactionForExpense;

public record GetTransactionForExpenseQuery(int ExpenseId) : IRequest<TransactionDto>
{

}

public class GetTransactionForExpenseQueryValidator : AbstractValidator<GetTransactionForExpenseQuery>
{
    public GetTransactionForExpenseQueryValidator()
    {
    }
}

public class GetTransactionForExpenseQueryHandler : IRequestHandler<GetTransactionForExpenseQuery, TransactionDto>
{
    private readonly IApplicationDbContext _context;

    public GetTransactionForExpenseQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionDto> Handle(GetTransactionForExpenseQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
