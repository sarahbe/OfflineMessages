using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Models
{
    public class UserBlockModel
    {
        public int ID { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string RecipientName { get; set; }

        public string RecipientId { get; set; }

    }
}