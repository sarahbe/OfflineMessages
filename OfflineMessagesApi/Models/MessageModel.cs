

using System.ComponentModel.DataAnnotations;

namespace OfflineMessagesApi.Models
{
    public class MessageModel
    {
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Body")]
        public string Body { get; set; }


        [Required]
        [Display(Name = "RecipientName")]
        public string RecipientName { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReciepentId { get; set; }

    }
}