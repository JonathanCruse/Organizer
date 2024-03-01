using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
using Organizer.Application.Common.Mappings;

namespace Organizer.Application.FinancialService.Queries.GetTransactionsForCollective;

public record GetTransactionsForCollectiveQuery(int CollectiveId) : IRequest<PaginatedList<TransactionDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetTransactionsForCollectiveQueryValidator : AbstractValidator<GetTransactionsForCollectiveQuery>
{
    public GetTransactionsForCollectiveQueryValidator()
    {
    }
}

public class GetTransactionsForCollectiveQueryHandler : IRequestHandler<GetTransactionsForCollectiveQuery, PaginatedList<TransactionDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;


    public GetTransactionsForCollectiveQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<PaginatedList<TransactionDto>> Handle(GetTransactionsForCollectiveQuery request, CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Where(x => x.Debtor.Id ==request.CollectiveId)
            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
