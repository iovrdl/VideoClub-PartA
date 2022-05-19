using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using VideoClub.Data.Entities;

namespace VideoClub.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public ApplicationDbContext()
            : base("VideoClubContext", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Film>()
                .HasMany(f => f.Copies)
                .WithRequired(c => c.Film)
                .WillCascadeOnDelete(false);

            builder.Entity<Rental>()
                .HasRequired(r => r.User)
                .WithMany(u => u.Rentals)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(builder);
        }
    }
}