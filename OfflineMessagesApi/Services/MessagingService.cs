using OfflineMessagesApi.DAL;
using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Services
{
    public class MessagingService : BaseService, IMessagingService
    {

        private readonly MessageContext _context;

        public MessagingService(MessageContext context)
        {
            _context = context;
        }

        public void CreateMessage(MessageModel message)
        {

            var newMessage = new Message
            {
                ID = message.ID,
                Body = message.Body,
                RecipientId = message.ReciepentId,
                SenderId = message.SenderId
            };

            _context.Messages.Add(newMessage);
            _context.SaveChanges();
        }
    }
}