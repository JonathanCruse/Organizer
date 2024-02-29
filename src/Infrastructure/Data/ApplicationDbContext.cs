using System.Reflection;
using Organizer.Application.Common.Interfaces;
using Organizer.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Organizer.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<Feminist>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TodoList> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<FeministCollective> FeministCollectives => Set<FeministCollective>();
    public DbSet<Feminist> Feminists => Set<Feminist>();
    public DbSet<Collective> Collectives => Set<Collective>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Expense> Expenses => Set<Expense>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
