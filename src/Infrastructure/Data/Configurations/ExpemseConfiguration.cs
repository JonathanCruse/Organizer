using Organizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Organizer.Infrastructure.Data.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {

        //builder
        //    .HasOne(x => x.Transaction)
        //    .WithMany(y => y.Expenses)
        //    .OnDelete(DeleteBehavior.NoAction);
    }
}
