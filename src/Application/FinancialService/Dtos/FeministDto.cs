using Organizer.Application.TodoLists.Queries.GetTodos;
using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Dtos;

public class FeministDto
{
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public IReadOnlyCollection<FeministsCollectivesDto> FeministsCollectives { get; set; } = new List<FeministsCollectivesDto>();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Feminist, FeministDto>();
        }
    }
}
