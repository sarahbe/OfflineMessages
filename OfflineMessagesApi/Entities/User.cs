using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;


namespace OfflineMessagesApi.Entities
{
    public class User : IdentityUser
    {
        /// <summary>
        /// Responsible to fetch the authenticated user identity from the database
        /// </summary>
        /// <param name="manager">The manager will contain the credintials</param>
        /// <param name="authenticationType">Authontication Type</param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}