using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
using Organizer.Application.Common.Mappings;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Queries.GetFeministsForCollective;

public record GetFeministsForCollectiveQuery(int CollectiveId) : IRequest<PaginatedList<FeministDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
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
    private readonly IMapper _mapper;
    private readonly IUser _user;


    public GetFeministsForCollectiveQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<PaginatedList<FeministDto>> Handle(GetFeministsForCollectiveQuery request, CancellationToken cancellationToken)
    {
        return await _context.Feminists
            .Where(x => x.FeministsCollectives.Any(feministCollective => feministCollective.Collective.Id == request.CollectiveId ))
            .ProjectTo<FeministDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
