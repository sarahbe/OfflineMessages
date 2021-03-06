﻿using OfflineMessagesApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace OfflineMessagesApi.Models
{
    /// <summary>
    /// Contain all the functions needed to shape the response object and control the object graph returned to the client.
    /// </summary>
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private ApplicationUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(Entities.User appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                EmailConfirmed = true,
                UserName = appUser.UserName,
                Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
                Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        public List<MessageReturnModel> GetMessages(List<Message> messages)
        {

            List<MessageReturnModel> messageList = new List<MessageReturnModel>();
            foreach (Message ms in messages)
            {

                messageList.Add(new MessageReturnModel
                {
                    ID = ms.ID,
                    SenderName = ms.Sender.UserName,
                    Timestamp = ms.Timestamp,
                    Body =  ms.Body.Substring(0, Math.Min(ms.Body.Length, 5)) + "..."
                });
            }
            return messageList;
        }

        public List<SentMessageReturnModel> GetSentMessages(List<Message> messages)
        {

            List<SentMessageReturnModel> messageList = new List<SentMessageReturnModel>();
            foreach (Message ms in messages)
            {
                messageList.Add(new SentMessageReturnModel
                {
                    ID = ms.ID,
                    SenderName = ms.Sender.UserName,
                    Timestamp = ms.Timestamp,
                    ReceivedDate = ms.ReceivedDate,
                    ReadDate = ms.ReadDate,
                    Body = ms.Body.Substring(0, Math.Min(ms.Body.Length, 5)) + "..."
                });
            }
            return messageList;
        }


        public MessageReturnModel GetMessage(Message message)
        {
            return new MessageReturnModel
            {
                SenderName = message.Sender.UserName,
                Timestamp = message.Timestamp,
                Body = message.Body
            };
        }
    }



    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime Birthdate { get; set; }
        public string MaritalStatus { get; set; }
        public string Job { get; set; }
        public IList<string> Roles { get; set; }
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }

    public class MessageReturnModel
    {
        public int ID { get; set; }
        public string SenderName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }

    }

    public class SentMessageReturnModel
    {
        public int ID { get; set; }
        public string SenderName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Body { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ReadDate { get; set; }
    }

}