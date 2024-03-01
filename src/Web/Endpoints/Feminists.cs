using Microsoft.AspNetCore.Mvc;
using Organizer.Application.Common.Models;
using Organizer.Application.FinancialService.Commands.CreateEpenseForBalance;
using Organizer.Application.FinancialService.Commands.InviteFeminist;
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

public class Feminists : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetFeminists)
            .MapGet(GetBalance, "Balance")
            .MapPost(CreateFeminist);
    }

    public Task<PaginatedList<FeministDto>> GetFeminists(ISender sender, [AsParameters] GetFeministsForCollectiveQuery query)
    {
        return sender.Send(query);
    }

    public Task<float> GetBalance(ISender sender)
    {
        return sender.Send(new GetBalanceForFeministQuery());
    }

    public Task CreateFeminist(ISender sender, InviteFeministCommand command)
    {
        return sender.Send(command);
    }

}
