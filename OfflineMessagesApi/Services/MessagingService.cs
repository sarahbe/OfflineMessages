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

        /// <summary>
        /// Create new message
        /// </summary>
        /// <param name="message"></param>
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

        /// <summary>
        /// Get Received Messages
        /// </summary>
        /// <param name="userId">Recipient Id</param>
        /// <returns></returns>
        public List<Message> GetReceivedMessages(string userId)
        {
            var messages = _context.Messages.Where(m => m.RecipientId == userId).OrderByDescending(m => m.Timestamp).ToList();
            return messages;
        }
        /// <summary>
        /// Get Sent Messages
        /// </summary>
        /// <param name="userId">Sender Id</param>
        /// <returns></returns>
        public List<Message> GetSentMessages(string userId)
        {
            var messages = _context.Messages.Where(m => m.SenderId == userId).OrderByDescending(m => m.Timestamp).ToList();
            return messages;
        }

        /// <summary>
        /// Get Message by message id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message GetMessage(int id)
        {
            var message = _context.Messages.First(m => m.ID == id);
            return message;
        }
    }
}