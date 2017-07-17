using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public bool Valid { get; set; } = true;
    }
}