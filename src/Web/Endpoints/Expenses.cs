﻿using Organizer.Application.Common.Models;
using Organizer.Application.FinancialService.Commands.CreateEpenseForBalance;
using Organizer.Application.FinancialService.Commands.DeleteEpenseForBalance;
using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.FinancialService.Queries.GetExpensesForFeminist;

namespace Organizer.Web.Endpoints;

public class Expenses : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetExpenses)
            .MapPost(CreateExpense)
            .MapDelete(DeleteExpense, "{id}");
    }

    public Task<PaginatedList<ExpenseDto>> GetExpenses(ISender sender, [AsParameters] GetExpensesForFeministQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateExpense(ISender sender, CreateEpenseForBalanceCommand command)
    {
        return sender.Send(command);
    }
    public async Task<IResult> DeleteExpense(ISender sender, int id)
    {
        await sender.Send(new DeleteEpenseForBalanceCommand(id));
        return Results.NoContent();
    }


}
