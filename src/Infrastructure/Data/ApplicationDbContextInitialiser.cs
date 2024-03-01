using Organizer.Domain.Constants;
using Organizer.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Organizer.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Feminist> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<Feminist> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new Feminist { UserName = "administrator@localhost", Email = "administrator@localhost" };
        var feminist1 = new Feminist { UserName = "feminist1@localhost", Email = "feminist1@localhost" };
        var feminist2 = new Feminist { UserName = "feminist2@localhost", Email = "feminist2@localhost" };
        var feminist3 = new Feminist { UserName = "feminist3@localhost", Email = "feminist3@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.CreateAsync(feminist1, "Administrator1!");
            await _userManager.CreateAsync(feminist2, "Administrator1!");
            await _userManager.CreateAsync(feminist3, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
                await _userManager.AddToRolesAsync(feminist1, new[] { administratorRole.Name });
                await _userManager.AddToRolesAsync(feminist2, new[] { administratorRole.Name });
                await _userManager.AddToRolesAsync(feminist3, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.TodoLists.Any())
        {
            _context.TodoLists.Add(new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list 📃" },
                    new TodoItem { Title = "Check off the first item ✅" },
                    new TodoItem { Title = "Realise you've already done two things on the list! 🤯"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap 🏆" },
                }
            });

            await _context.SaveChangesAsync();
        }

        List<Collective> collectives = [
                new Collective {
                    Name = "Collective 1"
                },
                new Collective {
                    Name = "Collective 2"
                },
            ];

        List<FeministCollective> feministCollectives = [
                new FeministCollective {
                    Feminist = feminist1,
                    Collective = collectives[0],
                    Balance = -2f
                },
                new FeministCollective {
                    Feminist = feminist2,
                    Collective = collectives[0],
                    Balance = 2f
                },
                new FeministCollective {
                    Feminist = feminist2,
                    Collective = collectives[1],
                    Balance = -500f
                },
                new FeministCollective {
                    Feminist = feminist3,
                    Collective = collectives[1],
                    Balance = 500f
                },
            ];
        collectives[0].CollectivesFeminists = [feministCollectives[0], feministCollectives[1]];
        collectives[1].CollectivesFeminists = [feministCollectives[2], feministCollectives[3]];

        List<Expense> expenses = [
                new Expense {
                    Amount = 2,
                    Debtor = feminist1,
                },
                new Expense {
                    Amount = -2,
                    Debtor = feminist2,
                },
                new Expense {
                    Amount = 500,
                    Debtor = feminist2,
                },
                new Expense {
                    Amount = -500,
                    Debtor = feminist3,
                },
            ];
        List<Transaction> transactions = [
            new Transaction {
                Amount = 4,
                Creditor = feminist2,
                Debtor = collectives[0],
            },
            new Transaction {
                Amount = 1000,
                Creditor = feminist3,
                Debtor = collectives[1],
            }
        ];
        expenses[0].Transaction = transactions[0];
        expenses[1].Transaction = transactions[0];
        expenses[2].Transaction = transactions[1];
        expenses[3].Transaction = transactions[1];
        if (!_context.Collectives.Any())
        {
            _context.Collectives.AddRange(collectives);
            _context.FeministCollectives.AddRange(feministCollectives);
            _context.Transactions.AddRange(transactions);
            _context.Expenses.AddRange(expenses);
            await _context.SaveChangesAsync();
        }
    }
}
