using Organizer.Application.Common.Models;
using Organizer.Application.FinancialService.Commands.CreateCollective;
using Organizer.Application.FinancialService.Commands.DeleteCollective;
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

public class Collectives : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCollectives)
            .MapPost(CreateCollective)
            .MapDelete(DeleteCollective, "{id}");
            ;
    }

    public Task<PaginatedList<CollectiveDto>> GetCollectives(ISender sender, [AsParameters] GetCollectivesForFeministQuery query)
    {
        return sender.Send(query);
    }
    public Task<int> CreateCollective(ISender sender, CreateCollectiveCommand command)
    {
        return sender.Send(command);
    }
    public async Task<IResult> DeleteCollective(ISender sender, int id)
    {
        await sender.Send(new DeleteCollectiveCommand (id));
        return Results.NoContent();
    }

    public async Task<IResult> DeleteSomethingElseCollective(ISender sender, int id)
    {
        await sender.Send(new DeleteCollectiveCommand(id));
        return Results.NoContent();
    }

}
