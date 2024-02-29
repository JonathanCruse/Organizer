using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;

namespace Organizer.Application.FinancialService.Queries.GetExpensesForFeminist;

public record GetExpensesForFeministQuery : IRequest<PaginatedList<ExpenseDto>>
{
}

public class GetExpensesForFeministQueryValidator : AbstractValidator<GetExpensesForFeministQuery>
{
    public GetExpensesForFeministQueryValidator()
    {
    }
}

public class GetExpensesForFeministQueryHandler : IRequestHandler<GetExpensesForFeministQuery, PaginatedList<ExpenseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetExpensesForFeministQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ExpenseDto>> Handle(GetExpensesForFeministQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
