using System;

namespace RAZE.Entities
{
    public class Request : BaseEntity
    {
        public string Identifier { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime ExpiresAt { get; set; }

        public virtual Account Sender { get; set; }
        public virtual Account Receiver { get; set; }
    }
}
