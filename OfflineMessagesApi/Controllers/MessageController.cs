﻿using OfflineMessagesApi.Models;
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

        /// <summary>
        /// Gets all received messages to the specified user.
        /// </summary>
        /// <param name="userId">Receipent</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReceived")]
        public IHttpActionResult GetMessagesByUserId(string userId)
        {
            var messages = _messageService.GetReceivedMessages(userId);
            _messageService.SetReceivedDate(messages);
            return Ok(this.TheModelFactory.GetMessages(messages));
        }

        /// <summary>
        /// Gets all sent messages from the specified user.
        /// </summary>
        /// <param name="userId">Sender ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSent")]
        public IHttpActionResult GetSentMessagesByUserId(string userId)
        {
            var messages = _messageService.GetSentMessages(userId);           
            return Ok(this.TheModelFactory.GetSentMessages(messages));
        }

        /// <summary>
        /// Get message by message ID. To read message.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IHttpActionResult GetMessage(int id)
        {
            var message = _messageService.GetMessage(id);

            if (message == null)
            {
                return NotFound();
            }
            _messageService.SetReadDate(message);
            return Ok(this.TheModelFactory.GetMessage(message));
        }

        /// <summary>
        /// Post a new message to the system.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> CreateMessageAsync(MessageModel message)
        {
            var reciepent = await this.AppUserManager.FindByNameAsync(message.RecipientName);
            if (reciepent == null)
            {
                throw new ApplicationException("Message not found");
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
