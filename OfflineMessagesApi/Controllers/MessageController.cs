using OfflineMessagesApi.Models;
using OfflineMessagesApi.Services;
using System;
using System.Web.Http;

namespace OfflineMessagesApi.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : BaseApiController
    {

        private IMessagingService _messageService;

        private IBlockingService _blockingService;

        public MessageController(IMessagingService msgService, IBlockingService blkService)
        {
            _messageService = msgService;
            _blockingService = blkService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetMessagesByUserId(string userId)
        {        
            var messages = _messageService.GetAllByUserId(userId);
            return Ok(this.TheModelFactory.GetMessages(messages));
        }


        [Route("Create")]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateMessageAsync(MessageModel message)
        {
            var reciepent = await this.AppUserManager.FindByNameAsync(message.RecipientName);
            if (reciepent == null)
            {
                throw new ApplicationException("User is not found");
            }
            message.ReciepentId = reciepent.Id;

            var blocked = _blockingService.ChechUser(message.SenderId, message.ReciepentId);

            if (!blocked)
            {
                _messageService.CreateMessage(message);

                Log.Debug($"User with id {message.SenderId} sent a message to {message.RecipientName}. ");
            }
            return Ok();
        }

    }
}
