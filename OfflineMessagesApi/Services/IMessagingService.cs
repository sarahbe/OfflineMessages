using OfflineMessagesApi.Models;

namespace OfflineMessagesApi.Services
{
    public interface IMessagingService
    {
        void CreateMessage(MessageModel message);
    }
}