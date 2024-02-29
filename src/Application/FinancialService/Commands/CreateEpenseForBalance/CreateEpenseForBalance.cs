using System.Reflection.Metadata.Ecma335;
using Organizer.Application.Common.Interfaces;

namespace Organizer.Application.FinancialService.Commands.CreateEpenseForBalance;

public record CreateEpenseForBalanceCommand(float Amount) : IRequest<int>
{

}

public class CreateEpenseForBalanceCommandValidator : AbstractValidator<CreateEpenseForBalanceCommand>
{
    public CreateEpenseForBalanceCommandValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Must be positive");
    }
}

public class CreateEpenseForBalanceCommandHandler : IRequestHandler<CreateEpenseForBalanceCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateEpenseForBalanceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateEpenseForBalanceCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
