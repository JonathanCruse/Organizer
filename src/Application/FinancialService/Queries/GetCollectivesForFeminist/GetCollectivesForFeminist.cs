using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;

namespace Organizer.Application.FinancialService.Queries.GetCollectivesForFeminist;

public record GetCollectivesForFeministQuery : IRequest<PaginatedList<CollectiveDto>>
{
}

public class GetCollectivesForFeministQueryValidator : AbstractValidator<GetCollectivesForFeministQuery>
{
    public GetCollectivesForFeministQueryValidator()
    {
    }
}

public class GetCollectivesForFeministQueryHandler : IRequestHandler<GetCollectivesForFeministQuery, PaginatedList<CollectiveDto>>
{
    private readonly IApplicationDbContext _context;

    public GetCollectivesForFeministQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<CollectiveDto>> Handle(GetCollectivesForFeministQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
