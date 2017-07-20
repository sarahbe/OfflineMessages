using OfflineMessagesApi.Models;
using OfflineMessagesApi.Services;
using System;
using System.Web.Http;

namespace OfflineMessagesApi.Controllers
{
    [RoutePrefix("api/blocking")]
    public class BlockingController : BaseApiController
    {
        private IBlockingService _blockingService;

        public BlockingController(IBlockingService service)
        {
            _blockingService = service;
        }

        /// <summary>
        /// Posts a user to be blocked.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Create(UserBlockModel model)
        {
            var reciepent = await this.AppUserManager.FindByNameAsync(model.RecipientName);
            if (reciepent == null)
            {
                throw new ApplicationException("User is not found");
            }
            model.RecipientId = reciepent.Id;
            _blockingService.BlockUser(model);
            return Ok();
        }
    }
}
