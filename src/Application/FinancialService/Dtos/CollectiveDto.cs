using Organizer.Domain.Entities;

namespace Organizer.Application.FinancialService.Dtos;

public class CollectiveDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IReadOnlyCollection<FeministsCollectivesDto> CollectivesFeminists { get; init; } = Array.Empty<FeministsCollectivesDto>();
    public IReadOnlyCollection<TransactionDto> Transactions{ get; init; } = Array.Empty<TransactionDto>();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Collective, CollectiveDto>();
        }
    }
}
