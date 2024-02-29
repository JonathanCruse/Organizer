using Organizer.Application.Common.Interfaces;

namespace Organizer.Application.FinancialService.Commands.CreateTransaction;

public record CreateTransactionCommand(string Description, float Amount) : IRequest<int>
{
}

public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(3)
            .MaximumLength(300)
            .WithMessage("Must contain between 3-300 letters");
        RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be positive");
    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }
    }
}
