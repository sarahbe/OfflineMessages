using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OfflineMessagesApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfflineMessagesApi.Services;
using OfflineMessagesApi.Entities;
using Microsoft.AspNet.Identity;
using OfflineMessagesApi.Models;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace OfflineMessagesApi.Controllers.Tests
{
    [TestClass()]
    public class MessageControllerTests
    {

        [TestMethod()]
        public void GetMessagesByUserIdTest()
        {
 
            var messagingServiceMock =new Mock<IMessagingService>();
            List<Message> allMessages = new List<Message>() {
            new Message{ID=1, Body = "First Message", RecipientId = "12", SenderId="13", Sender = new User{ Id="13" , UserName= "Sarah"} }
            };
            messagingServiceMock.Setup(s => s.GetReceivedMessages("12"))
                .Returns(allMessages);

            var blockingServiceMock = new Mock<IBlockingService>();
            var user = new User { Id = "13" , UserName= "Sarah" };
            var store = new Mock<IUserStore<User>>(MockBehavior.Strict);
            store.As<IUserStore<User>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            var request = new HttpRequestMessage();
            var controller = new MessageController(messagingServiceMock.Object, blockingServiceMock.Object);
            controller.AppUserManager = new ApplicationUserManager(store.Object);
            var modelFactory = new ModelFactory(request, controller.AppUserManager);
            controller.TheModelFactory = modelFactory;

            IHttpActionResult result = controller.GetMessagesByUserId("12");
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void GetMessageTest()
        {
            var messagingServiceMock = new Mock<IMessagingService>();
            List<Message> allMessages = new List<Message>() {
            new Message{ID=1, Body = "First Message", RecipientId = "12", SenderId="13", Sender = new User{ Id="13" , UserName= "Sarah"} }
            };
            messagingServiceMock.Setup(s => s.GetReceivedMessages("12"))
                .Returns(allMessages);

            var blockingServiceMock = new Mock<IBlockingService>();
            var user = new User { Id = "13", UserName = "Sarah" };
            var store = new Mock<IUserStore<User>>(MockBehavior.Strict);
            store.As<IUserStore<User>>().Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
            var request = new HttpRequestMessage();

            var controller = new MessageController(messagingServiceMock.Object, blockingServiceMock.Object);
            controller.AppUserManager = new ApplicationUserManager(store.Object);
            var modelFactory = new ModelFactory(request, controller.AppUserManager);
            controller.TheModelFactory = modelFactory;

            var result = controller.GetMessage(1);
            Assert.IsNotNull( result);

        }

    }
}