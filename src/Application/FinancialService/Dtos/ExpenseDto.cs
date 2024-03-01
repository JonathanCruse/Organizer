using Organizer.Domain.Entities;
namespace Organizer.Application.FinancialService.Dtos;

public class ExpenseDto
{
    public int Id { get; set; }
    public FeministDto Debtor{ get; set; } = new FeministDto();
    public float Amount { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public TransactionDto Transaction { get; set; } = new TransactionDto();
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Expense, ExpenseDto>();
        }
    }
}
