using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;

namespace Organizer.Application.FinancialService.Queries.GetFeministsForCollective;

public record GetFeministsForCollectiveQuery(int CollectiveId) : IRequest<PaginatedList<FeministDto>>
{
}

public class GetFeministsForCollectiveQueryValidator : AbstractValidator<GetFeministsForCollectiveQuery>
{
    public GetFeministsForCollectiveQueryValidator()
    {
    }
}

public class GetFeministsForCollectiveQueryHandler : IRequestHandler<GetFeministsForCollectiveQuery, PaginatedList<FeministDto>>
{
    private readonly IApplicationDbContext _context;

    public GetFeministsForCollectiveQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<FeministDto>> Handle(GetFeministsForCollectiveQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
