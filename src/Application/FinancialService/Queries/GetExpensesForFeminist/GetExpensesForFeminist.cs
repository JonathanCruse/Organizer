using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
using Organizer.Application.Common.Mappings;
using Organizer.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Organizer.Application.FinancialService.Queries.GetExpensesForFeminist;

public record GetExpensesForFeministQuery : IRequest<PaginatedList<ExpenseDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 1000;
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
    private readonly IMapper _mapper;
    private readonly IUser _user;
    private readonly IDistributedCache _cache;

    public GetExpensesForFeministQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user, IDistributedCache cache)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
        _cache = cache;
    }

    public async Task<PaginatedList<ExpenseDto>> Handle(GetExpensesForFeministQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ExpenseDto> expenses = _context.Expenses
        .Where(x => x.Debtor.Id == _user.Id)
        .OrderBy(x => x.LastModified)
        .ProjectTo<ExpenseDto>(_mapper.ConfigurationProvider);


        var result = await expenses
        .PaginatedListAsync(request.PageNumber, request.PageSize);

        return result;
    }
}
