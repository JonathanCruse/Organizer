using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;
using Organizer.Domain.Events;

namespace Organizer.Application.FinancialService.Commands.CreateTransaction;

public record CreateTransactionCommand(string Description, float Amount, int collectiveId) : IRequest<int>
{
}

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(300)
            .WithMessage("Must contain between 3-300 letters");
        RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be positive");
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;

        public CreateTransactionCommandHandler(IApplicationDbContext context, IUser user)
        {
            _context = context;
            _user = user;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction { 
                Description = request.Description,
                Amount = request.Amount,
                Creditor = _context.Feminists.Where(x => x.Id == _user.Id).First(),
                Debtor = _context.Collectives.Where(x => x.Id == request.collectiveId).First(),
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);
            transaction.GenerateExpenses();


            return transaction.Id;
        }
    }
}
