using Organizer.Application.Common.Exceptions;
using Organizer.Application.Common.Interfaces;
using Organizer.Application.Common.Models;

namespace Organizer.Application.FinancialService.Queries.GetBalanceForFeminist;

public record GetBalanceForFeministQuery : IRequest<float>
{
}

public class GetBalanceForFeministQueryValidator : AbstractValidator<GetBalanceForFeministQuery>
{
    public GetBalanceForFeministQueryValidator()
    {
    }
}

public class GetBalanceForFeministQueryHandler : IRequestHandler<GetBalanceForFeministQuery, float>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public GetBalanceForFeministQueryHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<float> Handle(GetBalanceForFeministQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        var user = _context.Feminists.Where(x => x.Id == _user.Id).First();
        if (user is null) throw new ForbiddenAccessException();

        var collectives = user.FeministsCollectives;
        if (collectives is null) return 0;


        return collectives.Sum(x => x.Balance);
    }
}
