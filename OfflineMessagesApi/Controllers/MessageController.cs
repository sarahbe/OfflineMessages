using OfflineMessagesApi.Models;
using OfflineMessagesApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OfflineMessagesApi.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageController : BaseApiController
    {


        private IMessagingService _messageService;

        public MessageController(IMessagingService service)
        {
            _messageService = service;
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

            _messageService.CreateMessage(message);

            return Ok();
        }

    }
}
