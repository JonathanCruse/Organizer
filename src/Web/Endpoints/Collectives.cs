using Microsoft.AspNetCore.Mvc;
using Organizer.Application.Common.Models;
using Organizer.Application.FinancialService.Commands.CreateCollective;
using Organizer.Application.FinancialService.Commands.DeleteCollective;
using Organizer.Application.FinancialService.Dtos;
using Organizer.Application.FinancialService.Queries.GetCollectivesForFeminist;

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

}
