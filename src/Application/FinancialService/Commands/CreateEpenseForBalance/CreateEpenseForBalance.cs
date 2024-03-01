using System.Reflection.Metadata.Ecma335;
using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Commands.CreateEpenseForBalance;

public record CreateEpenseForBalanceCommand(float Amount, int transactionId) : IRequest<int>
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
    private readonly IUser _user;

    public CreateEpenseForBalanceCommandHandler(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;
    }

    public async Task<int> Handle(CreateEpenseForBalanceCommand request, CancellationToken cancellationToken)
    {

        var expense = new Expense
        {
            Amount = (-1) * request.Amount,
            Transaction = _context.Transactions.Where(x => x.Id == request.transactionId).First(),
            Debtor = _context.Feminists.Where(x => x.Id == _user.Id).First()
        };
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync(cancellationToken);

        return expense.Id;
    }
}
