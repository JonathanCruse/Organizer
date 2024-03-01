using Organizer.Application.Common.Models;
using Organizer.Application.FinancialService.Commands.CreateCollective;
using Organizer.Application.FinancialService.Commands.CreateTransaction;
using Organizer.Application.FinancialService.Commands.DeleteCollective;
using Organizer.Application.FinancialService.Commands.DeleteTransaction;
using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.FinancialService.Queries.GetBalanceForFeminist;
using Organizer.Application.FinancialService.Queries.GetCollectivesForFeminist;
using Organizer.Application.FinancialService.Queries.GetExpensesForFeminist;
using Organizer.Application.FinancialService.Queries.GetFeministsForCollective;
using Organizer.Application.FinancialService.Queries.GetTransactionForExpense;
using Organizer.Application.FinancialService.Queries.GetTransactionsForCollective;
using Organizer.Application.TodoItems.Commands.CreateTodoItem;
using Organizer.Application.TodoItems.Commands.DeleteTodoItem;
using Organizer.Application.TodoItems.Commands.UpdateTodoItem;
using Organizer.Application.TodoItems.Commands.UpdateTodoItemDetail;
using Organizer.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace Organizer.Web.Endpoints;

public class Transactions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTransaction, "{id}")
            .MapGet(GetTransactions)
            .MapPost(CreateTransaction)
            .MapDelete(DeleteTransaction, "{id}");
    }

    public Task<PaginatedList<TransactionDto>> GetTransactions(ISender sender, [AsParameters] GetTransactionsForCollectiveQuery query)
    {
        return sender.Send(query);
    }
    public Task<TransactionDto> GetTransaction(ISender sender, int id)
    {
        return sender.Send(new GetTransactionForExpenseQuery(id));
    }

    public Task<int> CreateTransaction(ISender sender, CreateTransactionCommand command)
    {
        return sender.Send(command);
    }
    public async Task<IResult> DeleteTransaction(ISender sender, int id)
    {
        await sender.Send(new DeleteTransactionCommand (id));
        return Results.NoContent();
    }

}
