using Organizer.Domain.Entities;
namespace Organizer.Application.FinancialService.Dtos;

public class ExpenseDto
{
    public int Id { get; set; }
    public float Amount { get; set; }
    public DateTimeOffset LastModified { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Expense, ExpenseDto>();
        }
    }
}
