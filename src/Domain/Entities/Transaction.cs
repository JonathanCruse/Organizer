using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Domain.Entities;

namespace Organizer.Domain.Entities;
public class Transaction : BaseAuditableEntity
{
    public float Amount { get; set; }
    public string Description { get; set; } = String.Empty;
    public virtual Feminist Creditor { get; set; } = new Feminist();
    public virtual Collective Debtor { get; set; } = new Collective();
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
