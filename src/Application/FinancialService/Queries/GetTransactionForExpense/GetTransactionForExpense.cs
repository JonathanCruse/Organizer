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
    private readonly IMapper _mapper;
    private readonly IUser _user;


    public GetTransactionForExpenseQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<TransactionDto> Handle(GetTransactionForExpenseQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        return _context.Expenses
            .Where(expense => expense.Id == request.ExpenseId)
            .Select(expense => expense.Transaction)
            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
            .First();
    }
}
