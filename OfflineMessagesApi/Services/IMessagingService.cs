using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System.Collections.Generic;

namespace OfflineMessagesApi.Services
{
    public interface IMessagingService
    {
        void CreateMessage(MessageModel message);
         List<Message> GetAllByUserId(string userId);
    }
}