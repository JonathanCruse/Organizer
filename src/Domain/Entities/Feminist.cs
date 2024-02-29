using Organizer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Organizer.Domain.Entities;

public class Feminist : IdentityUser
{
    public float? MonthlyIncome { get; set; }
    public virtual ICollection<FeministCollective> FeministsCollectives { get; set; } = new List<FeministCollective>();

}
