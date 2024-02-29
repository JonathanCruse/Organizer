using Organizer.Application.Common.Interfaces;
using FluentValidation.Validators;

namespace Organizer.Application.FinancialService.Commands.CreateCollective;

public record CreateCollectiveCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}

public class CreateCollectiveCommandValidator : AbstractValidator<CreateCollectiveCommand>
{
    public CreateCollectiveCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(3)
            .MinimumLength(150)
            .WithMessage("Name must contain 3-150 letters");
    }
}

public class CreateCollectiveCommandHandler : IRequestHandler<CreateCollectiveCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCollectiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCollectiveCommand request, CancellationToken cancellationToken)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
