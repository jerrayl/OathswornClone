using System;

namespace RAZE.Models
{
    public class RequestModel
    {
        public string Sender { get; set; }
        public string ReceiverConnectionId { get; set; }
        public string RequestIdentifier { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}