using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Entities
{
    public class Message:BaseEntity
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Body { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ReadDate { get; set; }

        //Virtual Properties
        public virtual User Sender { get; set; }
        public virtual User Recipient { get; set; }
        
    }
}