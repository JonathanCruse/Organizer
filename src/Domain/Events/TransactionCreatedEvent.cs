using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizer.Domain.Events
{
    public class TransactionCreatedEvent : BaseEvent
    {
        public Transaction Transaction { get; }
        public TransactionCreatedEvent(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
