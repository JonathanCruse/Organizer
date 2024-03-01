using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;
using Organizer.Application.Common.Exceptions;
using Organizer.Domain.Entities;
using Organizer.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Organizer.Application.Common.Mappings;

namespace Organizer.Application.FinancialService.Queries.GetCollectivesForFeminist;

public record GetCollectivesForFeministQuery : IRequest<PaginatedList<CollectiveDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
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
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetCollectivesForFeministQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<PaginatedList<CollectiveDto>> Handle(GetCollectivesForFeministQuery request, CancellationToken cancellationToken)
    {
        return await _context.FeministCollectives
            .Where(x => x.Feminist.Id == _user.Id)
            .OrderBy(x => x.LastModified)
            .Select(x => x.Collective)
            .ProjectTo<CollectiveDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
