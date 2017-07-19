using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using OfflineMessagesApi.Entities;

namespace OfflineMessagesApi.DAL
{
   public class MessageContext : IdentityDbContext<User>
    {

        //name of connection string
        public MessageContext() : base("MessageContext")
        {

        }

        //The static method “Create” will be called from our Owin Startup class.
        public static MessageContext Create()
        {
            
            return new MessageContext();
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //preventint table names from being pluralized
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }
    }
}
