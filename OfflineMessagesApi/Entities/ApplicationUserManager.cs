using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using OfflineMessagesApi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OfflineMessagesApi.Entities
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<MessageContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<User>(appDbContext));

            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            return appUserManager;
        }
    }
}