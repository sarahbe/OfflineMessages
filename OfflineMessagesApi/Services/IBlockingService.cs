using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System.Collections.Generic;

namespace OfflineMessagesApi.Services
{
    public interface IBlockingService
    {
        void BlockUser(UserBlockModel model);
        bool ChechUser(string userId, string reciepentId);
    }
}