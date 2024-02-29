using Organizer.Domain.Entities;
using Organizers.Domain.Entities;

namespace Organizer.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    public DbSet<Collective> Collectives { get; }
    public DbSet<Feminist> Feminists{ get; }
    public DbSet<FeministCollective> FeministCollectives { get; }
    public DbSet<Transaction> Transactions { get; }
    public DbSet<Expense> Expenses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
