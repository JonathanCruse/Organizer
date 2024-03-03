using Organizer.Domain.Events;
using Microsoft.Extensions.Logging;
using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;

namespace Organizer.Application.TodoItems.EventHandlers;

public class TransactionCreatedEventHandler : INotificationHandler<TransactionCreatedEvent>
{
    private readonly ILogger<TransactionCreatedEventHandler> _logger;
    private readonly IApplicationDbContext _context;

    public TransactionCreatedEventHandler(ILogger<TransactionCreatedEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(TransactionCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.Transaction.Id == 0) return;
        var transaction = _context.Transactions.Where(t => t.Id == notification.Transaction.Id).First();

        if (transaction.Expenses.Count() > 0)
        {
            return;
        }
        var creditor = transaction.Creditor;

        var collective = transaction.Debtor;

        var mapping = collective.CollectivesFeminists.Select(x => new { Feminist =  x.Feminist, income = x.Feminist.MonthlyIncome ?? 0f, partition= 0f});
        var overallMonthlyIncome = mapping.Sum(x => x.income);
        if (mapping.Any(x => x.income == 0) ) {
            float eavenSplit = 1f / (float)mapping.Count(); ;
            mapping = mapping.Select(x => new { Feminist = x.Feminist, income = x.income, partition = eavenSplit });
        } else
        {
            mapping = mapping.Select(x => new { Feminist = x.Feminist, income = x.income, partition = x.income / (float)overallMonthlyIncome  });
        }

        _context.Expenses.AddRange(mapping.Select(x => new Expense { Amount = transaction.Amount * x.partition, Debtor = x.Feminist, Transaction = transaction }));
         await _context.SaveChangesAsync(cancellationToken);

        return;

    }
}
