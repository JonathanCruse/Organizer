using Organizer.Domain.Entities;
using Organizer.Application.FinancialService.Commands.CreateCollective;
using Organizer.Application.TodoLists.Queries.GetTodos;

namespace Organizer.Application.FinancialService.Dtos;

public class FeministCollectiveDto
{
    public float Balance { get; set; }
    public FeministDto Feminist { get; set; } = new FeministDto();
    public CollectiveDto Collective  { get; set; } = new CollectiveDto();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<FeministCollective, FeministCollectiveDto>();
        }
    }
}
