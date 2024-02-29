using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Domain.Entities;
public class Collective : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<FeministCollective> CollectivesFeminists { get; set; } = new List<FeministCollective>();


}
