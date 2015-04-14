using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace KonigLabs.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<CrewMember> CrewMembers { get; set; }        
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<ArticleCategory> ArticleCategories { get; set; }


    }
}