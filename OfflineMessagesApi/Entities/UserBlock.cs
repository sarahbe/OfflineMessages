using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Entities
{
    public class UserBlock:BaseEntity
    {
        public string UserId { get; set; }
        public string RecipientId { get; set; }

        //Virtual Properties
        public virtual User User { get; set; }
        public virtual User Recipient { get; set; }
    }
}