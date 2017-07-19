using OfflineMessagesApi.DAL;

namespace OfflineMessagesApi.Services
{
    public class BaseService
    {
        public MessageContext ctx;
        public BaseService()
        {
            ctx = new MessageContext();
        }
    }
}