﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Organizer.Domain.Entities;

namespace Organizer.Domain.Entities;
public class FeministCollective : BaseAuditableEntity
{
    public virtual Collective Collective { get; set; } = null!;
    public virtual Feminist Feminist { get; set; } = null!;
    public float Balance { get; set; }

}
