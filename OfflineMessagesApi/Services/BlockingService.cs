using OfflineMessagesApi.DAL;
using OfflineMessagesApi.Entities;
using OfflineMessagesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Services
{
    public class BlockingService : BaseService, IBlockingService
    {

        private readonly MessageContext _context;

        public BlockingService(MessageContext context)
        {
            _context = context;
        }

        public void BlockUser(UserBlockModel model)
        {
            var user = new UserBlock
            {
                ID = model.ID,
                UserId = model.UserId,
                RecipientId = model.RecipientId
            };
            _context.UserBlocks.Add(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// if blocked user exists, return true
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="reciepentId"></param>
        /// <returns></returns>
        public bool ChechUser(string userId, string reciepentId)
        {
            List<UserBlock> blockedUser = _context.UserBlocks.Where(m => m.RecipientId == reciepentId && m.UserId == userId).ToList();

            return blockedUser.Any();
        }

    }
}