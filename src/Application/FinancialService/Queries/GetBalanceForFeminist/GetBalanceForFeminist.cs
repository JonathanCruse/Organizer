using Microsoft.Extensions.Caching.Distributed;
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
    private readonly IDistributedCache _distributedCache;

    public GetBalanceForFeministQueryHandler(IApplicationDbContext context, IUser user, IDistributedCache distributedCache)
    {
        _context = context;
        _user = user;
        _distributedCache = distributedCache;
    }

    public async Task<float> Handle(GetBalanceForFeministQuery request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(_user.Id);
        var cacheKey = "balance-" + _user.Id;
        var cachedBalance = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        float sum;
        if (cachedBalance == null) {
            var user = _context.Feminists.Where(x => x.Id == _user.Id).First();
            if (user is null) throw new ForbiddenAccessException();

            var collectives = user.FeministsCollectives;
            if (collectives is null) return 0;

            sum = collectives.Sum(x => x.Balance);
            await _distributedCache.SetStringAsync(cacheKey, sum.ToString(), cancellationToken);
            return sum;
        }
        return (float)Convert.ToInt32(cachedBalance);
    }
}
