using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System;
using System.Collections.Generic;

namespace OfflineMessagesApi.Services
{
    public interface IMessagingService
    {
        void CreateMessage(MessageModel message);
        List<Message> GetReceivedMessages(string userId);
        List<Message> GetSentMessages(string userId);
        Message GetMessage(int id);
        void SetReceivedDate(List<Message> receivedMessages);
        void SetReadDate(Message message);
    }
}